# cli_template.py - Create a CLI project template
"""Template for command-line application projects."""

# TODO: Define REQUIREMENTS with CLI dependencies
REQUIREMENTS = """
# CLI Dependencies
# Add appropriate packages here
""".strip()

# TODO: Define project STRUCTURE
STRUCTURE = {
    # Add your directory structure here
}

# TODO: Define MAIN_CODE with argparse CLI skeleton
MAIN_CODE = '''
# Your CLI application code here
'''

def get_template():
    """Return complete CLI project template."""
    # TODO: Return template configuration dict
    pass

# Test your template
if __name__ == '__main__':
    template = get_template()
    print(f"Template: {template['description']}")
    print(f"Requirements:\n{template['requirements']}")