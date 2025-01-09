# Power

Clase abstracta que contiene todos los parámetros y métodos fundamentales para todos los poderes de los personajes.

>**expectation**: Los poderes pueden interactuar con el laberinto y los personajes, mediante la colocación de elementos dinámicos en el labrinto (trampas, fuego, veneno, ...) o aplicación de efectos en personajes (quemadura, envenenamiento, ...). Tanto los elementos dinámicos que pueden colocar como los efectos sobre personajes, son clases independientes con sus propias dinámicas. Cada clase que herada de BaseCharacterPower son poderes que estan compuestos únciamente por un método que determina su comportamiento luego de activarse.

## Propiedades

- **name** (string):
- **effect** (string)
- **cooldown** (int): cantidad de turnos necesarios para que la habilidad pueda volver a ser utlizada.
- **currentCooldown** (int): turno hasta el cual la habilidad no puede volver a ser usada.
- **energyCost** (int): cantidad de energía que necesita tener disponible una ficha para poder utlizar la habilidad.

## Métodos

- **+ Power** (string name): constructore de clase con un dicionario que contiene todas habilidades.
- **+ Activate** (int currentTurn): activa la habilidad reajustando su currentCooldown y devolviendo su efecto.
- **+ Cooldown**: get currentCooldown
- **+ EnergyCost**: get energyCost
