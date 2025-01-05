# Map

Mapa del juego, contiene una una matriz de la clase MapCell.

>**Note**: el mapa es cuadrado.

## Propiedades

- **obstacleDensity** (int): densidad de obtáculos, mayor valor representa mayor cantidad de obstáculos con respecto al tamaño del mapa.
- **map** (MapCell[,]): mapa del juego.

## Métodos

- **+ Map** (int dimension, int obstacleDensity, Character[] character): constructor de clase que genera el mapa y coloca los staticElement (coloca los muros exteriores del mapa), coloca las fichas.
  - **AddRandomElement**: añade elementos estáticos en el mapa de forma aleatoria.
  - **IsMapAccessible**: devuelve true si el mapa es accesible.
    - **CheckCells**: método recursivo que recorre el mapa analizando la accesibilidad.
- **+ Getmap**: get the map of game.
- **- AddInteractiveElement** (int turn): cada cierto intervalo de turnos agrega elementos interativos en el mapa de forma aleatoria.
