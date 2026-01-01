# cli_template.py - CLI project template
"""Template for command-line application projects."""

REQUIREMENTS = """
# CLI Dependencies
click>=8.1.0
rich>=13.0.0
pytest>=7.4.0
""".strip()

STRUCTURE = {
    'src': {
        'commands': {},
        'utils': {}
    },
    'tests': {},
    'docs': {}
}

MAIN_CODE = '''
import argparse
import sys

def main():
    parser = argparse.ArgumentParser(
        description='My CLI Application',
        formatter_class=argparse.RawDescriptionHelpFormatter
    )
    
    parser.add_argument('--version', action='version', version='1.0.0')
    parser.add_argument('-v', '--verbose', action='store_true', help='Enable verbose output')
    
    subparsers = parser.add_subparsers(dest='command', help='Available commands')
    
    # Add 'run' subcommand
    run_parser = subparsers.add_parser('run', help='Run the application')
    run_parser.add_argument('name', help='Name to greet')
    
    args = parser.parse_args()
    
    if args.command == 'run':
        print(f"Hello, {args.name}!")
    else:
        parser.print_help()

if __name__ == '__main__':
    main()
'''

def get_template():
    """Return complete CLI project template."""
    return {
        'requirements': REQUIREMENTS,
        'structure': STRUCTURE,
        'main_code': MAIN_CODE,
        'description': 'Command-line application with argparse'
    }

if __name__ == '__main__':
    template = get_template()
    print(f"Template: {template['description']}")
    print(f"Requirements:\n{template['requirements']}")