# Player

Clase que contiene todo lo referente a los usuarios, permitiendo la interacción usuario - hero.

## Propiedades

- **name** (string)
- **score** (int): puntuación del jugador.
- **hero** (Hero): ficha del jugador.

## Métodos

- **HeroController** (string HeroName): contructor que crea al hero y se lo asigna al player.
- **+ Move** (int direction): devuelve un bool que verifica si fue posible o no mover al héroe en la dirección seleccionada.
- **+ UsePower** (): devuelve un bool que verfica si fue posbile activar el poder del héroe.
