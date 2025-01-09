# Map

Mapa del juego, contiene una una matriz de la clase MapCell.

>**Note**: el mapa es cuadrado.

## Propiedades

- **mapRows**(int): almacena las dimensiones del mapa.
- **map** (MapCell[,]): mapa del juego.
- **density** (int): regula la densidad de elementos en el mapa, a mayor valor más elemenetos tendrá el mapa.

## Métodos

- **+ Map** (int dimension, Hero[] hero): constructor de clase que genera el mapa y coloca los staticElement (incluyendo los muros exteriores del mapa), coloca las fichas.
- **AddRandomElement** (string name): añade elementos estáticos en el mapa de forma aleatoria.
- **IsMapAccessible**: devuelve true si el mapa es accesible.
  - **CheckCells** (int x, int y, bool[] checkMask): método recursivo que recorre el mapa analizando la accesibilidad.
- **+ GetCell**: get  array de celdas.
- **+ AddGems**:  añade gemas de forma aleatoria en el mapa según la densidad.
- **+ AddTramps**: añade trampas de forma aleatorial en el mapa según la densidad.
