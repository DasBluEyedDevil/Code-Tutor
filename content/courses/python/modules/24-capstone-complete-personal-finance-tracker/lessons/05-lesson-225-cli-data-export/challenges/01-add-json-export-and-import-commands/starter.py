@export_app.command("json")
def export_json(
    output: Annotated[Path, typer.Argument(help="Output file path")],
):
    """TODO: Export all data to JSON backup."""
    pass


@app.command(name="import")
def import_json(
    input_file: Annotated[Path, typer.Argument(help="JSON file to import")],
    merge: Annotated[
        bool,
        typer.Option("--merge", help="Merge with existing data")
    ] = False,
):
    """TODO: Import data from JSON backup."""
    pass