# MapCell

Unidad básica del mapa que se encarga de contener los elementos del juego.

## Propiedades

- **occupant** (Hero?): almacena las fichas.
- **gameElement** (Element?): almacena elementos del juego.

## Métodos

- **+ IsAccessible** (): devuelve true si ocupante es null y gameElement es pasable o null.
- **+ AddOccupant** (Hero hero): añade ficha a occupant si occupant es null y gameElement pasable.
- **+ IsValidForPlacement** (): devuelve true si gameElement es null y occupant es null.
- **+ PlaceElement** (Element element): añade un GameElement a gameElement si IsValidForPlacement es true.
- **+ GetElement**:
- **+ RemoveElement** (): hace gameElement = null.
- **+ IsPassable** (): devuelve true si gameElement es pasable.
