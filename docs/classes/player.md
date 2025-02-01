# Player

Clase que contiene todo lo referente a los usuarios, permitiendo la interacción usuario - hero.

## Propiedades

- **name** (string)
- **score** (int): puntuación del jugador.
- **hero** (Hero): ficha del jugador.

## Métodos

- **HeroController** (string name): contructor que crea al player y le asigna un nombre.
- **+ Move** (int direction, int currentDuration): devuelve un bool que verifica si fue posible o no mover al héroe en la dirección seleccionada.
- **+ UsePower**: devuelve un bool que verfica si fue posbile activar el poder del héroe.
- **+ AddScore** (int points): agrega puntos al player.
- **+ AddHero** (Hero hero): agrega ub héroe al player.
- **+ Name**: get name.
- **+ GetHero**:
