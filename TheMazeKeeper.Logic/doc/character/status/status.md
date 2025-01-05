# Status

Clase que es el medio de interación de otras clase con las fichas.

## Propiedades

- **name** (string)
- **effects** (int \[3]): el array contiene valores numéricos que afectan los parámetros currentEnergy, currentIniciative, CurrentVisioRange respectivamente de las fichas. El efecto es beneficioso si los valores son positivos y perjudicial si son negativos.
- **duration** (int): duración del efecto en términos de turnos.

## Métodos

- **+ Status** (string name): constructor de clase que contiene un diccionario con todos los efectos del juego y sus parámetros.
- **+ GetEffect**:
- **+ GetDuration**:
- **+ InitDuration** (currentTurn): adiciona el turno actual a duration.
