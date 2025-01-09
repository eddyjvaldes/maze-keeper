# MapCell

Unidad básica del mapa que se encarga de contener los elementos del juego.

## Propiedades

- **occupant** (Hero?): almacena las fichas.
- **gameElement** (Element?): almacena elementos del juego.

## Métodos

- **+ PlaceElement** (Element element): añade un elemento a la celda si esta no contiene ningún elemento.
- **+ IsPassable** (): devuelve true si la celda no contiene elemento o si de contener uno este es pasable.
- **+ IsValidForPlacement** (): devuelve true si gameElement es null y occupant es null.
- **+ AddOccupant** (Hero hero): añade ficha a occupant si occupant es null y gameElement es null o pasable.
- **+ IsAccessible** (): devuelve true si ocupante es null y gameElement es pasable o null.
- **+ RemoveElement** (): hace gameElement = null.
- **+ HasElement** (): devuelve true si la casilla contiene algún elemento.
- **+ GetElement**:
- **+ GetOccupant**:
