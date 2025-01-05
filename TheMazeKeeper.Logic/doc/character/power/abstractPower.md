# AbstractPower

Clase abstracta que contiene todos los parámetros y métodos fundamentales para todos los poderes de los personajes. Los poderes pueden interactuar con el laberinto y los personajes, mediante la colocación de elementos dinámicos en el labrinto (trampas, fuego, veneno, ...) o aplicación de efectos en personajes (quemadura, envenenamiento, ...). Tanto los elementos dinámicos que pueden colocar como los efectos sobre personajes, son clases independientes con sus propias dinámicas. Cada clase que herada de BaseCharacterPower son poderes que estan compuestos únciamente por un método que determina su comportamiento luego de activarse.

## Propiedades

- **name** (string):
- **cooldown** (int): representa hasta que turno la habilidad no puede volver a ser utilizada.
- **currentCooldown** (int): representa el cooldown actual de la habilidad.
- **energyCost** (int): cantidad de energía que necesita tener disponible una ficha para poder utlizar la habilidad.

## Métodos

- **+ Activate** (Hero player, int currentTurn): cada vez que se activa la habilidad modifica currentCooldown.
- **+ GetCooldown**:
- **+ GetEnergyCost**:
