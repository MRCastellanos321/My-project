# Informe

Desarrollado en Unity version

# Tutorial de Juego:

En la pantalla de inicio se encuentra un display de la preview de la imagen de cada jugador, el tipo de ficha se cambia a través de las flechas y el botón "i" muestra el poder de cada una de ellas. Cuando la selección está completa, el botón jugar carga la escena del juego.

## Historia y Objetivo:
_Un grupo de aldeanos fue expulsado de su comunidad al convertirse misteriosamente en monstruos._ Dispersos por el laberinto se encuentran los fragmentos de alma, que deben coleccionar para recuperar su alma y convertirse otra vez humanos. Una vez conseguido su objetivo, deben encontrar el centro del laberinto, donde un portal los llevará de vuelta a casa. Por desgracia, la búsqueda los ha vuelto agresivos unos con otros por miedo a que no haya suficientes fragmentos de alma. Convertirse en monstruo dotó a cada uno de una habilidad especial, pero cuidado!: al convertirse de nuevo en humanos la pierden. Los jugadores deben sobrevivir en el laberinto lleno de trampas y cuidarse de los ataques de los demás jugadores mientras luchan por ser los primeros en completar la búsqueda y llegar a la casilla central.

## Turnos y Movimiento:
El turno de cada jugador está determinado por un "dado" que elige una cantidad al azar de pasos que puede dar, y muestra los restantes en pantalla. 
* Estos no se gastan al intentar pasar por obstáculos y ser negado el paso. 
* Cuando termine su turno, se muestra en la pantalla un cartel que pide presionar la tecla de espacio para realizar el cambio de cámaras y turnos(para dar un tiempo a realizar un intercambio de jugador frente a la pantalla y que sea posible observar si cayó en una trampa con su último paso, en lugar de una transición brusca). También puedes decidir acabar tu turno pronto con la flecha en la esquina de la pantalla.
* Puedes moverte por cualquier camino usando las teclas de las flechas(derecha, izquierda, arriba y abajo) y no puedes pasar por arriba de otros jugadores u óbstaculos.

## Ataque:
Si te acercas a un jugador e intentas moverte a su casilla con las flechas, aparecerá un botón de "atacar" el cual hará que el otro jugador tenga que saltarse cierta cantidad de turnos al presionarlo. Si el jugador ya fue incapacitado por otro jugador o una trampa, una advertencia aparecerá diciendo que el ataque no surgió efecto(para evitar que un jugador tenga que saltarse demasiados turnos) Además, los jugadores incapacitados aparecen de un color más azulado que el resto.
* Al acercarte tomas el riesgo de que el turno del otro jugador llegue pronto y seas tú el atacado. Así mismo, luego de realizar un ataque exitoso, no podrás efectuar otro hasta cierta cantidad de turnos(también saldrá una advertencia si lo intentas de todas formas).

## Poderes:
Cada tipo de ficha o monstruo tiene un poder que puede utilizar presionando un botón a la izquierda de la pantalla. Los tipos son:
1. Vampiro
2. Bruja
3. Fantasma
4. Hongo
5. Ninfa
6. Dragón
7. Vidente

* En el juego se explica cada cual en el botón "i" de la pantalla de inicio y en "Código y Estrategias" también.

* El poder tiene un tiempo de enfriamiento durante el cual el botón desaparecerá (al igual que si se convierte en humano) y en su lugar aparecerá una cuenta de cuántos turnos restan para poderlo utilizar.

* Si se requieren ciertas condiciones para usar el poder y estas no se cumplen, aparecerá una advertencia en pantalla, y no se aplicará tiempo de enfriamiento.

## Obstáculos: 
* No puedes pasar a través de las paredes del laberinto, sin embargo, hay un tipo de pared disimulada entre las demás a la que si intentas acercarte, aparece un botón que te permite romperla. Hacerlo te costará el resto de tus movimientos, y es posible que lleve a un callejón sin salida, pero también puede ser un atajo útil. 
* El centro del laberinto está rodeado por paredes pero en cada dirección cardinal hay una puerta cerrada con llave. Las cuatro llaves están dispuestas por el laberinto. Si te acercas a una puerta y tienes una llave, aparecerá un botón que te permitirá abrirla.

## Recoletables: 
Además de las cuatro llaves dispersas por laberinto, están los fragmentos de alma, de los cual cada jugador necesita 3 para convertirse en humano. Estos serán añadidos a tu inventario al pasar a través de ellos, luego de lo cual desaparecen. En el lado izquierdo de la pantalla hay una cuenta de cuántos has coleccionado hasta ahora. Hay otros dos coleccionables, uno que permite disminuir el enfriamiento del ataque y otro de la habilidad, ambos por un turno. Si tu tiempo de enfriamiento es 0(no hay penalización sobre el poder o el ataque) estos no se recolectarán.

## Trampas:
Hay 5 tipos de trampas. Estas se activan al pasar sobre ellas y una vez te hayan hecho efecto se desactivan y un camino de distinto color aparece donde estaba antes. Si tu habilidad te permite ser inmune a una trampa, esta te notificará que la has evadido, e igualmente se desactivará.

* Pierdes un fragmento recolectado: 

Si no tienes ninguno o ya te convertiste en humano, no hace efecto, y no se desactiva. De lo contrario disminuye tu cantidad de fragmentos de alma en 1.

* Desaparece el mapa del laberinto: 

Pierdes el mapa en la esquina superior de tu pantalla por varios turnos.

* Incapacitadora:

Tu turno se termina y se te saltarán los siguientes.

* Disminuye el dado: 

Divide a la mitad tus movimientos restantes actuales y lo continúa haciendo al empezar los siguientes turnos.

* Enfría el ataque: 

Aumenta tu cantidad actual de turnos de enfriamiento de ataque. 

Los efectos aplicados por trampas pueden revisarse a la derecha de la pantalla, donde aparecen tres íconos, un mapa, un dado, y una espada, que se volverán grises cuando estés bajo el efecto de una trampa que los daña. La espada también se volverá gris cuando ataques a otro jugador y estés bajo attackCoolDown.

## Estrategias y código:

### Backtracking:
---

La clase encargada de generar una representación del laberinto a través de una matriz, usando el algoritmo DFS, y luego ubicar a través de una codificación por números las diferentes trampas, obstáculos, y recolectables.

### Interfaz de tipos de ficha:
---

Cada ficha o tipo de monstruo tiene una clase propia que implementa la interfaz CharacterInterface. Las funciones del menú principal guardan un int que representa el tipo de ficha seleccionado y en función de ello se inicializa una instancia de cada clase solicitada y se guarda en un CharacterInterface[] array, lo cual permite acceder a las variables de cada instancia de clase y a sus funciones a través de la variable currentPlayerIndex de GameManager. La interfaz define la función común Skill(), programada diferente para cada personaje, y otras como GetSkillCoolDown() y SetSkillCoolDown(int value) que son las que usa el GameManager para modificar y leer el enfriamiento del poder. Además en el GameManager se crea un array que contiene el componente sprite de cada jugador y una lista de todos los posibles sprites, de los cuales se eligen los que se le va a asignar a cada objeto jugador a partir del int que devuelva el menú principal el menu.

1. Vampiro:
#### Al ejecutar su habilidad gana inmudidad a los dos siguientes ataques que reciba. Su función skill usa SetAttackInmunity(2);

2. Bruja:
#### Al ejecutar su habilidad se teletransporta a una casilla al azar en blanco, que no sea de los bordes o del centro. Lo que hace es cambiar su posición f y c en la matriz, y el PlayerMovement se encarga de transformar la posición del GameObject en el sistema de coordenadas

3. Fantasma:
#### Al ejecutar su habilidad, se llama a SetTrapInmunity(2). Las dos siguientes trampas en las que caiga no lo afectarán.

4. Hongo:
#### Al ejecutar su habilidad, se comienza a revisar si las posiciones f,c a su alrededor son iguales a las de algún otro jugador, y para cuando termina, todos los jugadores en su rango de ataque (5 casillas en todas las direcciones) son incapacitados.

5. Ninfa:
#### Similar al hongo, revisa las casillas a su alrededor, pero lo que hace es robar 1 shard a cada jugador en rango. Si llega a tres, se convierte en humano, pero los shards de los jugadores a su alrededor igualmente se disminuyen todos.

6. Dragón:
#### Multiplica por dos sus movimientos actuales, simplemente cambiando diceNumber.

7. Vidente
#### Si no ha caído en una trampa que desactive la visibilidad del mapa, hace aparecer un punto rojo que rastrea su posición por dos turnos.

### GameManager:
---
Es la clase que controla los turnos de los jugadores, que a su vez manejan las cámaras, enfriamiento, duración del efecto de las trampas y número de dado. GameManager también lleva cuenta de la posición actual de cada jugador con respecto a la matriz (coordenadas f y c). La función de movimiento válido usa una función para leer la matriz que creó Backtracking y revisar así el tipo de casilla a la que se quieren mover. Retorna falso o verdadero si se puede mover o no, pero también revisa si hay jugadores atacables, si se está acercando a una puerta, etc, para mostrar o no los botones que permiten llamar a las funciones correspondientes. La función FellInTrap() es la encargada de revisar dentro de la función de Unity Update() si el jugador actual está en una trampa y de cuál tipo es, así como de desactivar las trampas unas vez activadas. El GameManager también es el encargado de mostrar y cambiar o no las notificaciones en la pantalla y de convertir en Humano al jugador cuando recolecta 3 fragmentos, y en el update comprueba si alcanzó el centro del laberinto, y de ser así, si ya es o no humano. 

### SpawnMaze:
---

Es la función que pinta el laberinto a partir de la matriz de Backtracking, instancia los objetos recolectables, y contiene la función de SpawnTile() a veces llamada por el GameManager para poder convertir puertas y paredes rompibles en caminos.

### MazeCanvas:
---

Clase encargada de pintar el mapa del laberinto en la esquina superior derecha de la pantalla a partir de la matriz del backtracking. También permite implementar la habilidad del _vidente_ a través de una casilla roja que se actualiza con su posición

### Recolectables:
---

Cada tipo de objeto recolectable tiene un script asociado que permite usar el sistema de colisiones de Unity. OnTriggerEnter2D() va a revisar cuando colisionen dos objetos, si las condiciones de recolección se cumplen, y destruye el objeto de ser el caso.

### Player Movement:
---

Un script idéntico asociado a cada jugador con una variable pública playerIndex que se ajusta desde el inspector de Unity. El script comprueba si playerIndex == currentPlayerIndex de Game Manager, y si el jugador aún tiene movimientos. De ser así, llama a ValidMovement(), de GameManager, para revisar si la dirección que insertó el jugador a través de las flechas es válida. Calcula la posición a la que se tiene que dirigir y usa Lerp() para interpolar el movimiento y que sea una transición suave. También revisa la ejecución de la habilidad del personaje _bruja_ para que su teletransportación no afecte el update.
