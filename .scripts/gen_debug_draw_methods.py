import re
from pathlib import Path

BASE_PATH = Path('../Assets/DebugDraw/Runtime')
ITEMS_PATH = BASE_PATH / 'Items'
OUTPUT_FILE = BASE_PATH / 'DebugDrawMethods.cs'

MESH_TYPE_REGEX = re.compile(r'/\*\s+mesh:\s+(.+?)\s+\*/')
LOCAL_MESH_TYPE_REGEX = re.compile(f'^\s*{MESH_TYPE_REGEX.pattern}', MESH_TYPE_REGEX.flags)
GET_METHODS_REGEX = re.compile(
    r'((?:///(?:[^\n\r]+)\s+)+)(\[.+?\])?\s*public\s+static\s+(.+?)\s+Get([^(].+?)?'
    r'\((.+?)\)\s*{\s*(.+?)\s*}',
    re.DOTALL)
GET_WRAPPER_REGEX = re.compile(r'^\s*return\s+Get\((.+)\);')
PARAM_REGEX = re.compile(r'\s*(ref\s+)?(.+?\s+)([^,\s]+)(\s*=\s*[^,]+)?,?')
DEFAULT_REGEX = re.compile(r'\s*=\s*[^,)]+')
INDENT_REGEX = re.compile(r'\t\t')


def run():
    # Validate
    if not OUTPUT_FILE.is_file():
        print('Output file does not exist.')
        return
    if not ITEMS_PATH.is_dir():
        print('Items folder does not exist.')
        return
    
    static_methods = []
    instance_methods = []
    
    # Collect item get methods
    for item_path in ITEMS_PATH.glob('*.cs'):
        if not item_path.is_file():
            continue
        if item_path.stem in ('BaseItem', 'BasePointItem', 'IPointItem', 'ItemPool'):
            continue
        
        text = INDENT_REGEX.sub('\t', item_path.read_text('utf-8'))
        m = MESH_TYPE_REGEX.search(text)
        if not m:
            print(f'!! Missing "mesh: TYPE" definition for {item_path.stem}')
            continue
        
        default_mesh_type = m.group(1)

        for m in GET_METHODS_REGEX.finditer(text):
            docs, attribs, return_type, get_type, params, body = m.groups()
            clean_params = params.replace('\n', ' ').replace('\t', '')
            clean_params = DEFAULT_REGEX.sub('', clean_params)
            print(f'-- {return_type} {get_type} ({clean_params})')

            mesh_type = default_mesh_type
            func_name = return_type
            get_type = get_type or ''
            
            if get_type:
                if get_type == 'Wire':
                    func_name = get_type + func_name
                else:
                    func_name = get_type
            
            call_params = []
            refless_params = []
            for param_m in PARAM_REGEX.finditer(params):
                ref, v_type, name, val = [(p or '').strip() for p in param_m.groups()]
                call_params.append(' '.join(filter(None, [ref, name])))
                refless_params.append(' '.join(filter(None, [v_type, name, val])))

            type_m = LOCAL_MESH_TYPE_REGEX.match(body)
            if type_m:
                mesh_type = type_m.group(1)
            elif get_type == 'Wire':
                mesh_type = 'line'
            
            if not GET_WRAPPER_REGEX.match(body):
                body = f'return Get({", ".join(call_params)});'

            params = ', '.join(refless_params)

            static_body = GET_WRAPPER_REGEX.sub(
                fr'return {mesh_type}MeshInstance.Add('
                fr'DebugDrawItems.{return_type}.Get{get_type}(\g<1>));', body)
            instance_body = GET_WRAPPER_REGEX.sub(
                fr'return Add(DebugDrawItems.{return_type}.Get{get_type}(\g<1>));', body)
            
            method_groups = [
                (' static', static_methods, static_body),
                ('', instance_methods, instance_body),
            ]
            
            if attribs:
                attribs += '\n\t'
            
            meta = ''.join(filter(None, [docs, attribs]))
            for static, method_list, body in method_groups:
                method_out = [
                    meta,
                    f'public{static} {return_type} {func_name}({params})\n\t{{\n\t\t{body}\n\t}}',
                ]
                method_list.append(''.join(method_out))
            pass

    # Generate output
    output_text = OUTPUT_FILE.read_text()
    
    for group, methods in (('Static', static_methods), ('Instance', instance_methods)):
        group_re = re.compile(
            fr'(/\* <{group}GenMethods> \*/)\s+(?:.+)\s+(/\* </{group}GenMethods> \*/)',
            re.DOTALL)
        method_text = '\n\t\n\t'.join(methods)
        output_text = group_re.sub(fr'\1\n\t\n\t{method_text}\n\t\n\t\2', output_text)
    
    OUTPUT_FILE.write_text(output_text)
    pass


if __name__ == '__main__':
    run()
