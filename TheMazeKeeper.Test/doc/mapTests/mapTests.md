# MapTest

Tests que permite saber si la creación del mapa es correcta.

## Tests

- **TestGenerateMap_CreatesValidMap** (int mapDimension): realiza verificaciones en el mapa con varias dimensiones de prueba.

  - verfica si todos los bordes exteriores del mapa son muros.
  - verfica si el mapa tiene al menos un obstáculo.
  - verifica si el mapa es accesible.
- **TestHeroPlacementOnMap** (int mapDimension, int numberOfHeroes): realiza verificaciones en los héroes con mapas de diferentes medidas y diferente cantidad de héroes.

  - verifica si los héroes estan colocados en la posición esperada.
  - verifica si la celda donden estan los héroes no esta bloqueada.
