# Status

Clase que es el medio de interación de otras clase con las fichas.

## Propiedades

- **name** (string)
- **effectsEnergy** (int): efecto que causa la habilidad sobre la energía del héroe.
- **effectsInicaitive** (int): efecto que causa la habilidad sobre la iniciativa del héroe.
- **duration** (int): cantidad de turnos mínimos necesarios para poder utilizar la habilidad después de cada uso.
- **currentDuration** (int): turno hasta el cual dura el efecto sobre el héroe.

## Métodos

- **+ Status** (string name, int CurrentTurn): constructor de clase que contiene un diccionario con todos los efectos del juego y sus parámetros.
- **+ Effect**: devuelve un array con todos los efectos.
- **+ RebootDuration** (int currentTurn): reinicia la duración de la habilidad a partir del turno actual
- **+ Name**: get name
- **+ Duration**: get currentDuration
