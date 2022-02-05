import re
from pathlib import Path

BASE_PATH = Path('../Assets/DebugDraw/Runtime')
INPUT_FILE = BASE_PATH / 'Log.cs'
OUTPUT_FILE = BASE_PATH / 'LogShowMethods.cs'

GET_METHODS_REGEX = re.compile(
    r'((?:///(?:[^\n\r]+)\s+)+)(\[.+?\])?\s*public\s+static\s+void\s+'
    r'Print(.*?)\s*(<.+?>)?\((.+?)\)\s*{\s*(.+?)\s*}\n',
    re.DOTALL)
PARAM_REGEX = re.compile(r'.+?,\s*(.+?)\s*,.+', re.DOTALL)
CAST_REGEX = re.compile(r'\(object\)\s*')
DOCS_REGEX = re.compile(r'Logs (.+?)to the Unity Console')
GEN_SECTION_REGEX = re.compile(r'(/\* <ShowGenMethods> \*/).+(/\* </ShowGenMethods> \*/)', re.DOTALL)
BODY_TPL_CALL = 'LogMessage.Add(id, duration, {});'
BODY_TPL = """#if DEBUG_DRAW
\t\tif (DebugDraw.hasInstance)
\t\t{{{{
\t\t\t{}
\t\t}}}}
\t\t#endif
""".format(BODY_TPL_CALL)


def run():
    # Validate
    if not INPUT_FILE.is_file():
        print('Input file does not exist.')
        return
    if not OUTPUT_FILE.is_file():
        print('Output file does not exist.')
        return

    text = INPUT_FILE.read_text('utf-8')
    method_list = []

    for m in GET_METHODS_REGEX.finditer(text):
        docs, attribs, print_suffix, generics, params, body = m.groups()
        
        if print_suffix == 'Exception':
            continue
        if 'Object context' in params:
            continue

        print_suffix = print_suffix or ''
        generics = generics or ''
        docs = DOCS_REGEX.sub(r'Prints \1on the screen', docs)
        docs = docs.replace(
            '/// </summary>',
            f'/// </summary>\n\t'
            f'/// <param name="id">If non-zero, a unique key to prevent the same message from '
            f'being added multiple times.</param>\n\t'
            f'/// <param name="duration">How long to display the message, in seconds. '
            f'Pass 0 to only display for the next frame.</param>')
        
        if print_suffix == 'Format':
            body = 'string.Format(format, args)'
            pass
        else:
            body = PARAM_REGEX.sub(r'\1', body)
            pass
        
        if \
                body.startswith('GetString(') or \
                body.startswith('GetArgString(') or \
                params == 'object message':
            body = f'(string) {body}'

        body = BODY_TPL.format(CAST_REGEX.sub('', body))
        method_list.append(
            f'{docs}'
            f'public static void Show{print_suffix}{generics}'
            f'(int id, float duration, {params})\n\t{{\n'
            f'\t\t{body}\n'
            f'\t}}')
        pass
    
    methods_output = '\n\t\n\t'.join(method_list)
    
    text = OUTPUT_FILE.read_text('utf-8')
    text = GEN_SECTION_REGEX.sub(fr'\1\n\t\n\t{methods_output}\n\t\n\t\2', text)
    OUTPUT_FILE.write_text(text)
    
    pass


if __name__ == '__main__':
    run()
