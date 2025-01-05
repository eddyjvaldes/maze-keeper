# MapCell

Unidad básica del mapa que se encarga de contener los elementos del juego.

## Propiedades

- **occupant** (object?): almacena las fichas.
- **gameElement** (object?): almacena elementos del juego.

## Métodos

- **+ IsAccessible**: devuelve true si ocupante es null y gameElement es pasable o null.
- **+ IsValidForPlacement**: devuelve true si gameElement es null.
- **+ IsBlock**: devuelve true si gameElement es no pasable.
- **+ AddOccupant** (Hero): añade fichaa occupant si IsAccesible es true.
- **+ ClearOccupant**: hace occupant igual null.
- **+ PlaceElement** (GameElement): añade un GameElement a gameElement si IsValidForPlacement es true.
- **+ RemoveElement** (GameElement): hace gameElement igual null.
