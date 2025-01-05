# Hero

Clase que define las fichas jugables (héroes) por los players. La clase la componen los parámetros y comportamientos fundamentales, además de instancias de la clase power, que define las habilidades especiales de la ficha, y una lista de instancias de la clase status que define los efectos que actualmente afectan a la ficha.

## Propiedades

- **player** (player): jugador que constrola la ficha.
- **name** (string):
- **position** (Vector2): posición de la ficha en el mapa.

>**note**: La energía límita la cantidad de acciones que la ficha puede realizar por turnos, debido a que la mayoría de métodos reducen la energía para poder utilizarse.

- **maxEnergy** (int): energía máxima de la ficha por turno.
- **currentEnergy** (int): energía actual de la ficha en el turno.

- **iniciative** (int): determina el orden de juego de cada ficha, las fichas con mayor iniciative juegan primero.
- **currentIniciative** (int): iniciative actual de la ficha en el turno.

- **visionRange** (int): radio máximo del rango de visión del mapa que alcanza la ficha.
- **currentVisionRange** (int): visionRange actual de la ficha en el turno.

- **power** (Power): poder especial de la ficha
- **listStates** (Status): lista de effectos activos que afectan a la ficha cada turno.

## Métodos

- **+ hero** (string name, int x, int y): constructor de clase que contiene un diccionario con todos los héroes del juego y sus parámetros.
- **+ GetPlayer**:
- **+ GetName**:
- **+ GetPosition**:
- **+ GetCurrentEnergy**:
- **+ GetIniciative**:
- **+ GetVisionRange**:
- **+ HeroRecovery** (): restablece currentEnergy, currentIniciative y currentVisionRange al incio de cada turno.
- **+ GetStates** (): aplica efectos de todos los Status en listStates al player mientras currentEnergy, currentIniciative y currentVisionRange sean mayor o igual que cero. Si algún Status término su duración lo remueve.
- **+ MoveTo** (int direction): mueve la ficha una celda en la direción selecionada si la energía lo permite y la celda no esta ocupada, de ser así reduce la energía de la ficha en uno. Adémas de activar elementos en la casilla de estar disponibles.
- **+ UsePower** (int currentTurn): activa el poder de la ficha si la energía de la ficha lo permite, de ser así reduce la energía de esta según el costo del poder.
