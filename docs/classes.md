# Classes

Clases del juego agrupadas por proyectos y namespaces.

## TheMazeKeeper.Console

Projecto encargado de manejar la interación entre la lógica del juego y la representación visual.

- Program: clase principal

### Game

Grupo de clases encargadas de manejar los parámetros necesarios para iniciar y ejecutar el juego.

- [GameController](/docs/classes/gameController.md): maneja toda la representación visual necesaria para complemetar con la lógica del juego.
- [GameInit](/docs/classes/gameInit.md): interactúa con el usuario para obtener los parámetros necesarios para iniciar el juego.

## Logic

Projecto encargado de manejar la lógica del juego.

### GameManagement

Grupo de clases que se encargan de manejar lo referente a los jugadores.

- [Payer](/docs/classes/player.md): representa a los jugadores y define las acciones posibles a realizar por estos.

### GameCharacter

Grupo de clases que interactuan directamente con los fichas (heroes)

- [Hero](/docs/classes/gameCharacter/hero.md): contiene los parámetros y métodos fundamentales necesarios para el funcionamiento de la ficha. Cada ficha contiene en sus campos una instacia de la clase `Power`.
- [Power](/docs/classes/gameCharacter/power.md): contiene todos los poderes posibles a utilizar por las fichas. Estos poderes una vez activado aplican un efecto de `Status` sobre la ficha.
- [Status](/docs/classes/gameCharacter/status.md): son el medio de interación de las clases con la clase `Hero`, establece estados con duración que afectan a las fichas.

### GameElement

Grupo de clases que se encargan de manejar los elementos elementos que ocupan lugar en el mapa.

- [Element](/docs/classes/gameElement/element.md): clase abstracta que contiene los parámetros comunes de todos los elementos del juego.
- [Static](/docs/classes/gameElement/static.md): clase que hereda de `Element`, define todos los elementos que no interactúan con las fichas. Son estáticos en el mapa.
- [Gem](/docs/classes/gameElement/interactive/gem.md): clase que hereda de `Element`, son elementos en el mapa que otorgan puntos a los jugadores cuando sus fichas lo recogen.
- [Tramp](/docs/classes/gameElement/interactive/tramp.md): clase que hereda de `Element`, son elementos en el mapa que aplican efectos desfavorables en los héroes cuando estos se encuentran en la misma casilla.

### MapStructure

Grupo de clases que se encargan de manejar todo lo referente con el mapa del juego.

- [Map](/docs/classes/mapStructure/map.md): contiene una matriz de `MapCell` en sus campos, además de métodos para manejar todo lo referente a la creación y manipulación de elementos en el mapa.
- [MapCell](/docs/classes/mapStructure/mapCell.md): unidad básica del mapa, es capaz de contener fichas y elementos en sus campos.
