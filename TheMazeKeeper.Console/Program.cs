using TheMazeKeeper.Console;

GameInit init = new GameInit();
GameController game = new GameController(init.GetPlayers, init.GetMap, init.MapRow, 35);

//Pasa algo con los poderes de los personajes, controlar mejor la generacion de trampas y gemas, manejar todos los aspectos tecnicos con mayor profundidad