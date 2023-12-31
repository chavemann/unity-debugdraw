import re
from pathlib import Path

BASE_PATH = Path('../Assets/DebugDraw/Runtime')
INPUT_FILE = BASE_PATH / 'Log.cs'
OUTPUT_STATIC_FILE = BASE_PATH / 'LogTextMethods.cs'
OUTPUT_INSTANCE_FILE = BASE_PATH / 'LogMessageTextMethods.cs'

GET_METHODS_REGEX = re.compile(
    r'((?:///(?:[^\n\r]+)\s+)+)(\[.+?\])?\s*public\s+static\s+void\s+'
    r'Print(.*?)\s*(<.+?>)?\((.+?)\)\s*{\s*(.+?)\s*}\n',
    re.DOTALL)
PARAM_REGEX = re.compile(r'.+?,\s*(.+?)\s*,.+', re.DOTALL)
CAST_REGEX = re.compile(r'\(object\)\s*')
DOCS_REGEX = re.compile(r'Logs (.+?)to the Unity Console')
GEN_SECTION_REGEX = re.compile(r'(/\* <TextGenMethods> \*/).+(/\* </TextGenMethods> \*/)', re.DOTALL)
STATIC_BODY_TPL_CALL = 'return DebugDraw.hasInstance ? LogMessage.Add("", null, {}) : null;'
STATIC_BODY_TPL = """#if DEBUG_DRAW
\t\t\t{}
\t\t#else
\t\treturn null;
\t\t#endif""".format(STATIC_BODY_TPL_CALL)
INSTANCE_BODY_TPL = 'return SetText({});'
INSTANCE_BODY_REGEX = re.compile(r'(GetString|GetArgString|GetDictString|GetKeyValuePairsString)\(')


def run():
    # Validate
    if not INPUT_FILE.is_file():
        print('Input file does not exist.')
        return
    if not OUTPUT_STATIC_FILE.is_file():
        print(f'Static methods output file {OUTPUT_STATIC_FILE.name} does not exist.')
        return
    if not OUTPUT_INSTANCE_FILE.is_file():
        print(f'Instance methods output file {OUTPUT_INSTANCE_FILE.name} does not exist.')
        return

    text = INPUT_FILE.read_text('utf-8')
    static_method_list = []
    instance_method_list = []

    for m in GET_METHODS_REGEX.finditer(text):
        docs, attribs, print_suffix, generics, params, body = m.groups()
        
        if print_suffix == 'Exception':
            continue
        if 'Object context' in params:
            continue

        print_suffix = print_suffix or ''
        generics = generics or ''
        docs = DOCS_REGEX.sub(r'Displays \1on the screen', docs)
        
        if print_suffix == 'Format':
            body = 'string.Format(format, args)'
            pass
        else:
            body = PARAM_REGEX.sub(r'\1', body)
            pass

        if \
                body.startswith('GetString(') or \
                body.startswith('GetArgString(') or \
                body.startswith('GetDictString(') or \
                body.startswith('GetKeyValuePairsString(') or \
                params == 'object message':
            body = f'(string) {body}'

        static_body = STATIC_BODY_TPL.format(CAST_REGEX.sub('', body))
        static_method_list.append(
            f'{docs}'
            f'public static LogMessage Text{print_suffix}{generics}'
            f'({params})\n\t{{\n'
            f'\t\t{static_body}\n'
            f'\t}}')

        instance_body = CAST_REGEX.sub('', body)
        instance_body = INSTANCE_BODY_REGEX.sub(fr'Log.\1(', instance_body)
        instance_body = INSTANCE_BODY_TPL.format(instance_body)
        instance_method_list.append(
            f'{docs}'
            f'public LogMessage Text{print_suffix}{generics}'
            f'({params})\n\t{{\n'
            f'\t\t{instance_body}\n'
            f'\t}}')
        pass
    
    static_methods_output = '\n\t\n\t'.join(static_method_list)
    instance_methods_output = '\n\t\n\t'.join(instance_method_list)
    
    print(OUTPUT_STATIC_FILE.resolve())
    text = OUTPUT_STATIC_FILE.read_text('utf-8')
    text = GEN_SECTION_REGEX.sub(fr'\1\n\t\n\t{static_methods_output}\n\t\n\t\2', text)
    OUTPUT_STATIC_FILE.write_text(text)
    
    text = OUTPUT_INSTANCE_FILE.read_text('utf-8')
    text = GEN_SECTION_REGEX.sub(fr'\1\n\t\n\t{instance_methods_output}\n\t\n\t\2', text)
    OUTPUT_INSTANCE_FILE.write_text(text)
    
    pass


if __name__ == '__main__':
    run()
