# Hero

Clase que define las fichas jugables (héroes) por los players. La clase la componen los parámetros y comportamientos fundamentales, además de instancias de la clase power, que define las habilidades especiales de la ficha, y una lista de instancias de la clase status que define los efectos que actualmente afectan a la ficha.

## Propiedades

- **player** (Player): player que controla el héroe.
- **name** (string):
- **position** (Vector2): posición de la ficha en el mapa.

>**note**: La energía límita la cantidad de acciones que la ficha puede realizar por turnos, debido a que la mayoría de métodos reducen la energía de la ficha para poder utilizarse.
>
>**maxEnergy** (int): energía máxima de la ficha por turno.
>**currentEnergy** (int): energía actual de la ficha en el turno.

- **iniciative** (int): determina el orden de juego de cada ficha, las fichas con mayor iniciative juegan primero.
- **currentIniciative** (int): iniciative actual de la ficha en el turno.
- **power** (Power): poder especial de la ficha
- **listStates** (Status): lista de effectos activos que afectan a la ficha cada turno.

## Métodos

- **+ hero** (Player player, string name, int x, int y): constructor de clase que contiene un diccionario con todos los héroes del juego y sus parámetros.
- **+ MoveDirection** (Vector2  direction, MapCell[,] map): mueve la ficha a una celda en la direción selecionada si esta es accesible, de ser posible el movimiento, disminuye la energía del personaje en uno y si la nueva celda contiene algún elemento interactivo le aplica sus efectos al héroe.
- **+ UsePower** (int currentTurn): activa el poder de la ficha si la energía de la ficha lo permite, de ser así reduce la energía de esta según el costo del poder.
- **+ HeroRecovery** (): restablece los valores por currentEnergy y currentIniciative de la ficha.
- **+ UpdateStatusEffects** (int currentTurnj): aplica efectos de todos los Status en listStates al player. Si algún Status término su duración lo remueve.
- **ApplyStatusEffects** (Status status): aplica los efectos de un estado a los parámetros de la ficha.
- **+ ApplyNewStatus** (string statusName, int currentTurn): agrega y aplica un estado sobre el player.
- **+ Name**: get name.
- **+ Position**: get position.
- **+ GetEnergy**: get currentEnergy
- **+ GetIniciative**: get currentIniciative.
- **+ GetPower**: get power
