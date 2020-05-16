# Projeto 1 da Disciplina de Linguagens de Programação 1

## Felli

Marco Domingos, 21901309  
Daniel Fernandes, a21902752

## Autoria

Nesta secção será indicado exatamente o que cada aluno fez no projeto.

### O que foi feito por Marco Domingos

#### Classe `Program`
O pouco que está escrito em `Main()` (instanciação de `Game()` e a entrada no
mesmo) foi escrito por Marco Domingos.

#### Classe `UI`

Essencialmente todos os métodos foram criados por Marco Domingos, excepto o
método `ShowBoard()`.

Toda a documentação (com a exceção de alguns comentários em `ShowBoard()`) 
também foi criada por Marco Domingos.

#### Classe `Board`

Método `PlacePieces()`, `UpdateSimple()` e `UpdateEnemy()` foram criados por 
Marco Domingos. O renomeio da propriedade `GameOver` para `IsGameOver` foi
também feito por Marco Domingos.

A documentação foi iniciada por Daniel Fernandes, mas refinada e complementada
(com mais documentação XML e comentários, por exemplo) por Marco Domingos.

#### Classe `Tile`

Método `IsSurrounded()` feito por Marco Domingos.

A questão da documentação é igual a classe referida anteriormente (`Board`)

#### Classe `Game`

Feita primariamente por Marco Domingos. As variáveis de instância da classe, 
assim como os métodos `Initiate()`, `BeginGame()`, `ConvertStringToPos()` e
`UpdateGame()` foram criados por Marco Domingos.

A documentação foi, também, toda feita por Marco Domingos.

#### Classe `Position`

Apenas documentada por Marco Domingos.

#### Enum `Tilestate`

Criada e documentada por Marco Domingos.

#### Enum `ErrorCode`

Tudo criado e documentado por Marco Domingos, excepto `WrongTurn`.

#### Enum `MoveList`

Idem à `Tilestate`.

#### Relatório

Feito completamente por Marco Domingos.

#### Documentação por *Doxygen*

Feita completamente por Marco Domingos.

### O que foi feito por Daniel Fernandes

#### Classe `UI`

Método `ShowBoard()` quase inteiramente feito por Daniel Fernandes. A parte
em que o método imprime os números relacionados aos índices do tabuleiro foi
feita por Marco Domingos.

Daniel também adicionou uma nova mensagem de erro em `ErrorMessage()`, que 
diz que o jogador não pode selecionar uma peça que não é dele.

#### Classe `Board`

Todas as variáveis e propriedades foram criadas por Daniel Fernandes. Os únicos
métodos que não foram criados por Daniel Fernandes foram `PlacePieces()`, 
`UpdateSimple()` e `UpdateEnemy()`, porém este atualizou ambos os *Updates* para
utilizarem `NextTurn` ao invés dum player dado para o método. 

Daniel também corrigiu um *bug* em `UpdateEnemy()` em que o jogo não retirava
um `blackNum` ou `whiteNum` do jogador que perdeu uma peça.

#### Classe `Tile`

Todas as variáveis e propriedades foram criadas por Daniel Fernandes. O único
método não criado por Daniel Fernandes foi `IsSurronded()`.

#### Classe `Game`

Daniel Fernandes alterou a parte final de `BeginGame()`, que verifica quando
um jogador ganhou para imprimir o tabuleiro.

Tabmém foram feitas pequenas perguntas sobre certas funcionalidades das outras 
(nomeadamente `Board` e `Tile`) para implementação no *game loop* de 
`BeginGame()` (como, por exemplo, a funcionalidade da propriedade de turnos)
para Daniel Fernandes por Marco Domingos, mesmo que não estejam visíveis no
repositório.

#### Classe `Position`

Tudo, excepto a documentação, foi feita por Daniel Fernandes.

#### Enum `ErrorCode`

Fez `WrongTurn`.

#### Diagrama UML

Feito completamente por Daniel Fernandes.

**[Link para o repositório *Github*](https://github.com/condmaker/lp1_proj2)**

## Arquitetura da Solução

Este programa foi realizado com *5 classes* (sem contar a `Program`), sendo 
estas `Board`, `Game`, `Position`, `Tile` e `UI`, assim como *3 enumerações*,
sendo estas `ErrorCode`, `MoveList` e `Tilestate`. Como as enumerações são 
símples e fácilmente entendíveis, serão apenas explicadas estas 5 classes.

### Classe `Board`

Esta classe irá tratar de tudo que tem a ver com o tabuleiro atual-- sua 
construção, organização das *tiles* do tabuleiro, verificação de turnos, 
jogadores daquela instância do tabuleiro, estado de jogo (se jogador X ou Y 
ganhou, ou se ninguém ainda ganhou), a colocação de *Neighbours* para cada 
*tile*, retirada de uma referência duma peça a partir da sua posição, e a 
atualização do tabuleiro em si. 

*Neighbours*, no programa, são as *tiles* adjacentes de uma tile especifica.  
As variáveis de classe são:

`private Tile corBoard[]`, um *array* de `Tile`s. É o *tabuleiro*, e não contém o 
centro.

`private Tile center`, uma variável `Tile` que irá representar o centro do 
tabuleiro.

`private Tilestate firstPlayer, secondPlayer`, dois *enums* `Tilestate` que 
representam o primeiro e o segundo jogador respetivamente-- quem é o jogador
branco e quem é o jogador preto.

`private byte whiteNum, blackNum`, o número de peças brancas e pretas no 
tabuleiro. É utilizada primariamente para verificação de `GameOver` e `Winner`.

Agora, para as propriedades:

`public int Turn{ get; set; }`, irá contar os turnos da partida desta instância
do tabuleiro. Pode ser modificada de fora para funcionar com o resto do *game*
*loop* na classe `Game`. Também irá ser utilizada para decidir qual jogador
joga em que turno.

`public Tilestate NextTurn`, com apenas uma expressão `get`, irá observar se 
o turno é par/ímpar e decidir que jogador joga a partir disto, se o jogo não 
estiver acabado.

`public bool IsGameOver`, que verifica com apenas um `get` se não existem mais
peças brancas ou pretas no tabuleiro.

`public Tilestate Winner`, que verifica se o jogo acabou, e após isto vê se 
sobraram `Tile`s brancas ou pretas no tabuleiro, decidindo o vencedor pela(s)
cor(es) da(s) peça(s) que sobrou/sobraram.

#### Método `public Board()`

É o construtor da classe. Quando chamado, irá definir ambos `whiteNum` e 
`blackNum` como 6, o turno `Turn` como 0, e irá chamar o método `CreateBoard()`
para construir o tabuleiro da instância.

#### Método `private void CreateBoard()`

Irá construir o tabuleiro, definindo `corBoard` como um *array* de `Tile`s
bidimensional de 4x3, e logo após construindo dois ciclos *for*, um dentro do
outro (representando as linhas e colunas), e colocando um `Tile` em cada ponto 
do *Array*, com seu índice `index` já definido. Após isto, ele irá definir o 
centro como uma nova instância de `Tile` com índice 6.

Quando este processo de construção do tabuleiro for feito, o método irá chamar
`PlacePieces()` para mudar o estado das peças "de cima" para pretas e as "de 
baixo" para brancas, e `SetNeighbours()` para definir exatamente os 
*Neighbours* de cada `Tile` do tabuleiro.

#### Método `private void SetNeighbours()`

Irá, a partir do mesmo conjunto do duplo ciclo *for* em `CreateBoard()` 
(utilizado aqui também para passar sobre todas as `Tile`s do tabuleiro) para
verificar matemáticamente (por subtração e adição de *x* e *y*, que representam
colunas e linhas) as `Tile`s adjacentes àquela, guardando-as num novo *Array* 
de `Tile`s nomeado `aux`. 

Após a definição feita, a propriedade `Neighbours` da
`Tile` de momento irá ser atualizada com `aux`. No fim de cada ciclo que 
verifica em *x*, a variável `centerAux` (que também estava a ser verificada e 
foi declarada no início do programa) irá atualizar um dos *Neighbours* do centro
do tabuleiro, fazendo isto até o fim do duplo ciclo, ou seja, até atualizar 
todos.

#### Método `private void PlacePieces()`

Este método simplesmente irá iterar com uma variável `short i`, 
inicializada com -1, as vezes que repetiu o ciclo *foreach* a seguir. Este
ciclo irá passar por `corBoard`, apanhando todas as tiles, e caso `i` seja menor
que 6, esta `Tile` terá sua propriedade `State` como `Tilestate.Black`, e 
caso contário como `Tilestate.White`.

#### Método `public Tile GetTile(Position coord)`

Simplesmente, com a posição dada, retorna uma referência da `Tile` daquela 
posição específica no tabuleiro.

#### Método `public void UpdateSimple(Tile current, Tile after)`

Um dos métodos de *update* do tabuleiro. Trata-se de uma movimentação símples
de peça, sem "pular" ou "comer" uma peça inimiga. Simplesmente troca o estado
da `Tile current` para `Tilestate.Empty` e o da `Tile after` para o estado do
joador atual com `NextTurn`; 

#### Método `public void UpdateEnemy(Tile current, Tile after)` 

Um método de *update* utilizado quando um jogador vai "comer" uma peça inimiga.
Faz o mesmo que o método de *update* anterior com `Tile current` e `Tile after`,
mas verifica o `Tile` entre `current` e `after` para deixá-lo com um estado 
`Tilestate.Empty`.

Finalmente, o método verifica quem é que jogou naquele turno e retira uma peça 
de `blackNum` ou `whiteNum`.

#### Método `public void SelectPlayersTurn(Tilestate firstPlayer)`

Define, a partir de `Tilestate firstPlayer` de qual cor é que vai ser o primeiro
jogador, e coloca a cor inversa para o outro.

### Classe `Tile`

Esta classe irá tratar de cada `Tile` individual no tabuleiro já criado em 
`Board`. É possível verificar muito relacionado a uma tile específica, como se 
ela está encurralada, se pode mover perante outras `Tile`s, e apanhar a `Tile`
entre ela e mais outra.

Para suas variáveis:

`public int index`, é o índice da `Tile`-- um dos tipos de posição.

Para suas propriedades:

`public Tilestate State{get; set;}`, é o estado atual da `Tile` específica--
se está vazia (`Tilestate.Empty`), com uma peça preta (`Tilestate.Black`), ou
com uma peça branca (`Tilestate.White`).

`public Tile[] Neighbours{get; set;}`, irá guardar os *neighbours* desta tile
(suas referências) num *array* de `Tile`s.

Para seus métodos:

#### Método `public Tile(int index)`

Inicializa a instância de `Tile`. Recebe apenas o seu índice e dá `set` neste.

#### Método `public MoveList CanMoveBetweenTile(Tile targetTile, Tilestate ...)`

Recebe uma `targetTile` e verifica se esta tile em específico consegue ir para
lá, de acordo com o jogador do turno (`playerState`, a segunda variável de 
entrada do método). 

Esta faz isto percorrendo os `Neighbours` da `Tile` e definindo inicialmente que 
o tipo de retorno será `MoveList.Impossible` na variável `canMove`, reescrevendo 
isto caso sejá possível mover ao longo do método.

Primeiro é verificado se a `targetTile` tem o mesmo index do que um deles, 
definindo o retorno como `MoveList.Possible`. Caso não, esta verifica com o 
método `GetTileBetween()` se é possível "comer" um inimigo, e define o retorno 
como `MoveList.Enemy`. Caso algum destes ainda for possível, mas o estado de 
`targetTile` não for vazio, ele retorna `MoveList.Impossible`.

#### Método `public bool IsSurrounded()`

Verifica se todos os `Neighbours` daquela `Tile` são diferentes de 
`Tilestate.Empty`, e retorna `true` caso sejam (está rodeado) ou `false` caso
contrário.

#### Método `public Tile GetTileBetween(Tile target, Tilestate playerState)`

Irá apanhar o `Tile` entre a `Tile` de instância e `target`.

Ele verifica isto com um duplo *foreach* que verifica os `Neighbours` da `Tile`
de instância e de `target`, e depois vê quais `Neighbours` eles têm em comum
com a comparação de índices. Caso tenham, ele irá fazer testes finais, como
se estão na mesma linha/coluna, se está na lista de possibilidades de 'entre
`Tile`s' com `CheckBetweenPossibilities()`, e caso a variável `aux` depois 
destes testes ficar `true`, `betweenTile` é definido como àquela `Tile`, e 
retornado depois.

#### Método `public bool CheckBetweenPossibilities(Position pos1, Position pos2)`

Irá observar, entre duas posições, qual a lista de possibilidades verdadeiras
entre uma `Tile` estar entre elas.

### Classe `UI`

Esta classe irá tratar da verificação de *inputs* e de qualquer funcionalidade
a nível visual (com `Console.WriteLine()`s estando puramente aqui e mais em 
nenhum lado do programa).

As suas propriedades são:

`public string Input { get; private set; }`, inicializada com `""` para ficar
vazia, irá guardar e tratar de todos os *inputs* de jogo.

`public string[] SplitInput { get; private set;}`, é um *array* de *strings* 
que irá guardar *inputs* que foram separados, para análise mais fácil destes.

Os seus métodos:

***OBS: Os métodos com 'Message' no início deles sempre irão dar output com um
`Console.WriteLine()`.***

#### Método `private void MessageStart()`

Diz o que os jogadores tem de fazer no início do jogo (selecionar quem é quem).

#### Método `public void MessageCommands()`

Mostra a lista de comandos do jogo.

#### Método `public void MessagePieceChosen()`

Diz que o jogador selecionou uma peça com sucesso.

#### Método `public void MessageSurroundWarning()`

Alerta o jogador sobre a possibilidade de ser impossível de se mover com uma 
peça escolhida.

#### Método `public void MessageWinGame(Tilestate winner)`

Diz que jogador é que ganhou o jogo no final a partir de `Tilestate winner`.

#### Método `public void MessageGoodbye()`

Dá adeus aos jogadores.

#### Método `public void MessageTurn(int turn, Tilestate player)`

Mostra o turno atual (a partir de `int turn`) e que jogador é que vai jogar 
neste turno (a partir de `Tilestate player`).

#### Método `public Tilestate BeginningLoop()`

Vai definir a cor do jogador inicial. Apanha o input a partir de 
`WriteOnString()`, o separa com `SplitString()` e verifica com `InputCheck()`
se o jogador escreveu corretamente e qual cor é que escolheu, e o retorno de
`InputCheck()` será guardado em `returnCode`.

Caso `returnCode` seja 1, significa que o usuário teve um input de 'q', e ele
retornará `Tilestate.Empty`.

Caso seja 2, significa que o usuário não deu os comandos de forma correta.

Se o return code não for nenhum destes dois, o método verifica qual é o 
segundo elemento (`[1]`) de `SplitInput` e retorna `Tilestate.Black` ou 
`Tilestate.White` dependendo se o *input* foi *"black"* ou *"white"*.

#### Método `public void MainMenu()`

Imprime a partir de vários `Console.WriteLine()`s o menu principal, com os 
respetivos comandos.

#### Método `public void ShowTutorial()`

Através de vários `Console.WriteLine()`s e uma instância de `Board` para ser 
utilizada como exemplo, isto irá imprimir o tutorial de jogo. O tutorial é 
dividido em secções, e utiliza o método `ContinueTutorial()` para observar 
o *input* do jogador e ver se ele quer continuar. Caso retorne `false`, o método
retorna, e caso retorne `true`, ele continua com o resto do tutorial.

#### Método `private bool ContinueTutorial()`

Utiliza `Console.WriteLine()`s para imprimir uma mensagem para que se o jogador
deseja continuar ele precisa dar o *input* como 'c'. Depois disto, o input é 
lido e caso for 'c' ele retorna `true`, e em todo o resto `false`.

#### Método `private ushort InputCheck(string comm1, string comm2)`

Compara as *strings* `comm1` e `comm2` com o `SplitInput` da instância.

Antes de tudo, irá ser verificado se `SplitInput[0]` é igual a 'q', e se este
for o caso irá retornar 1.

`comm1` irá ser comparado então com o primeiro elemento de `SplitInput`, e 
se for diferente irá retornar 2 com uma mensagem de erro *Unknown Input*, 
pelo método `ErrorMessage()`.

O mesmo acontece para `comm2` e o segundo elemento de `SplitInput`.

Se nenhum dos casos acima for legítimo, ele então irá retornar 0.

#### Método`private ushort InputCheck(string comm1, string comm2, string comm3)`

*Overload* do método anterior. A única diferença é que `comm3` é verificado 
junto com `comm2` para o segundo elemento de `SplitInput`.

#### Método `public ushort InputFirstCheck(string comm1, string comm2)`

Bem parecido aos outros métodos de *input*. Primeiramente verifica se 
`SplitInput[0]` é igual a 'q', e retorna 1 caso verdadeiro.

Verifica então se `comm1` e `comm2` são diferentes de `SplitInput[0]`, e retorna
2 caso sejam, com uma mensagem de erro de tipo *Unknown Input* com o método
`ErrorMessage()`.

Caso `comm2` seja igual à `SplitInput[0]`, porém, o método irá retornar 3.

E novamente, caso nenhum dos casos acima for legítimo, retornará 0.

#### Método `public void ShowBoard(Board board, bool emptyMode = false)`

A partir de uma instância `Board`, retorna uma representação visual desta.
A segunda variável de entrada, `emptyMode` é opcional, mas caso seja dada e 
seja `true`, a representação visual do tabuleiro sairá vazio-- sem nenhuma peça.
Isto é utilizado para o tutorial, que necessita mostrar um tabuleiro vazio.

Ele então cria uma `Position pos` e um `char displayChar` que irão servir para
criar uma posição a partir dum índice e utilizar este índice para procurar um
uma `Tile` e utilizar o `State` desta `Tile` para criar um `char`, com o método
`StateToChar()`, respetivamente.

É então utilizado um ciclo *for*, que irá correr 12 vezes, e que imprime todas
as `Tiles` com o seu respetivo `displayChar`, coluna por coluna (`i < 3` é a
primeira coluna, `i < 6` é a segunda `i == 6` é o 'meio', etc...).

#### Método `private char StateToChar(Tilestate state)`

Recebe um `Tilestate` e retorna um `char` '.' se for `Tilestate.Empty`, 'B' se
for `Tilestate.Black`, e 'W' se for `Tilestate.White`.

#### Método `public void WriteOnString()`

Utiliza um `Console.ReadLine()` para ler o input do jogador e o guarda na 
propriedade `Input` da instância. Também utiliza um `Console.Write(">")` antes
por motivos de estética visual.

#### Método `public void SplitString()` 

Utiliza o método de instância duma string `Split()` em `Input` com a variável
de entrada " " para separar o *input* tendo os espaços como referência. Na 
mesma linha, isto é igualado à `SplitString`.

Ele após isto verifica se `SplitString` têm mais de 4 elementos, e se este for
o caso, limpa o *array* e dá uma mensagem de erro *Unknown Input* com 
`ErrorMessage()`.

#### Método `public void ErrorMessage(ErrorCode errorNumb)`

Recebe um *enum* `ErrorCode errorNumb`, e utiliza um *switch case* para 
verificar qual erro é que tem de imprimir.

### Classe `Position`

Uma classe básica. Irá tratar de posições (em linhas e colunas) e da conversão
destas em índices de `Tile`.

As suas propriedades são:

`public int Row { get; private set;}`, será o número da linha da atual posição.

`public int Col { get; private set;}`, será o número da coluna da atual posição.

Os seus métodos: 

#### Método `public Position(int row, int col)`

É o construtor da classe. Será utilizado para já dar valores as propriedades
Row e Col.

#### Método `public Position IndToPos(int ind)`

Recebe um `int`, que será uma índice de `Tile`, e irá reescrever Row e Col 
de acordo com a índice com um *switch case*. Retorna ele mesmo.

### Classe `Game`

Esta classe irá lidar com a instância de jogo e com o *game loop* em sí, 
comunicando-se com as classes `UI` e `Board` para criar isto.

As suas variáveis são:

`private Board gameBoard`, uma instância de `Board` para ser utilizada no jogo

`private UI userInterface`, uma instância de `UI` para ser utilizada no jogo

Os seus métodos:

#### Método `public void Initiate()`

O único método que pode ser chamado de fora da classe. Inicializa o menu 
principal e com um ciclo *while*, que irá rodar até que o `Input` do jogador (da 
instância de `UI`) seja diferente de 'q'. Antes de tudo, ele irá chamar o método
`MainMenu()` de `UI`, para mostrar ao jogador o ecrã principal com todos os
comandos possíveis.

O ciclo primeiramente irá guardar o *input* do jogador com o método 
`WriteOnString()` da instância de `UI`, utilizar um switch case para verificar
qual é que foi este input.

Caso seja 'm', irá imprimir o menu principal novamente com `MainMenu()` de `UI`.

Caso seja 't', irá mostrar o tutorial com `ShowTutorial()` de `UI`.

Caso seja 's', ele irá entrar no método `BeginGame()`.

Caso não seja nenhum destes, ele irá dar uma mensagem de erro do tipo *Unknown*
*Input* a partir de `ErrorMessage()` de `UI`.

Quando o ciclo for quebrado, ele simplesmente irá dizer adeus para o jogador 
com o método `MessageGoodbye()` de `UI`.

#### Método `private void BeginGame()` 

É o método que contém o *game loop* principal. As suas variáveis são:

`ushort inputCheck`, que contém um número para verificação de inputs

`Tilestate playerColor`, utilizado antes do inicio do jogo para verificar qual
cor o primeiro jogador escolheu

`Tile currentPos, nextPos`, para serem guardados os `Tile`s escolhidos pelos 
*inputs* dos jogadores

Antes do jogo iniciar, é iniciado o método `BeginningLoop` de `UI` para decidir
qual jogador terá qual cor. Isto será guardado em `playerColor`. É então 
verificado se a cor do jogador é `Tilestate.Empty`, que é inválido, dando assim
uma mensagem de erro por `ErrorMessage()` de `UI` e sairá do método. Caso 
contrário será comunicado à instância de `Board` a cor do primeiro jogador a 
partir do método `SelectPlayersTurn()`.

O *game loop* inicia-se aqui, que é um ciclo *while* que irá rodar enquanto o
input do jogador é diferente de 'q' e quando o jogo ainda não estiver acabado
(com a propriedade `IsGameOver` de `Board`).

O loop irá alternar suavemente dependendo do turno, e irá imprimir as mensagens
de turno (`MessageTurn()` de `UI`), o tabuleiro (`ShowBoard()` de `UI`) e os
comandos de jogo (`MessageCommands()`, novamente de `UI`). O jogador então irá
dar o seu *input*, que será separado com `SplitString()`.

Será verificado então, com `InputFirstCheck()` se o jogador escreveu algo válido
e o resultado será guardado em `inputCheck`. Caso o *check* for igual à 1, 
ele irá sair do *loop* (input foi 'q'), caso for 2, irá dar uma `ErrorMessage()`
de *Illegal Operation*, e irá retornar ao início do *loop*, e caso for 3, 
significa que o jogador escolheu o comando 'pass', que irá iterar o turno e 
passar para o turno do próximo jogador.

Após estes *checks*, o input de jogador será convertido em `Tile` com 
`ConvertStringToPos()` e `GetTile()` de `Board`, e depois será verificado
se a `Tile` escolhida pelo jogador é diferente daquela do turno dele, dando uma
`ErrorMessage()` de *Wrong Turn* e retornando ao início. Após isto, será 
verificado se a `Tile` do jogador está encurralada, e uma mensagem de aviso
será dada ao jogador caso isto for verdadeiro.

Se nenhum destes outros *checks* forem ativados, será comunicado ao jogador
que a peça dele foi escolhida, e irá pedir novamente `Input` para mover tal 
peça. É então separado e verificado o *input* da mesma forma que o passo 
passado, e então guardado a posição em `nextPos`. Será então chamado o método
`UpdateGame()` com `currentPos`, `nextPos` e `NextTurn` da instância de `Board`
para atualizar o tabuleiro.

Quando o loop é quebrado, então, é verificado se o jogo acabou com `IsGameOver`,
e caso foi, ele imprime o tabuleiro com `ShowBoard()` e o vencedor com 
`MessageWinGame()`, com `Winner` de `Board` entrando como o parâmetro do método.

#### Método `private Position ConvertStringToPos(string strCoord)`

Recebe uma string e cria uma `Position posCoord`. Utiliza `Int.Parse()` para
converter a string numa `int` e dá este valor a `int posIndex`, que é utilizado
depois em `IndToPos()`, utilizado em `posCoord`. Ele irá retornar `posCoord`
com a posição correta do índice, em formato de posição de linhas e colunas.

#### Método `private void UpdateGame(Tile currentTile, Tile afterTile, Tiles..)`

Recebe dois `Tile`s, um onde uma peça do jogador está atualmente, e outra onde
o jogador quer que esta peça se desloque para. O terceiro argumento é uma
`Tilestate currentPlayer`, que irá identificar o jogador atual.

O método então utiliza um *switch case*, com sua condição sendo a chamada do
método `CanMoveBetweenTile()` na instância de `currentTile`, que irá verificar
se o jogador pode ir à tile (`UpdateSimple()` de `Board`), não pode ir 
(`ErrorMessage()` de *Illegal Move*), ou se é movimentação do tipo *enemy*
(`UpdateEnemy()` de `Board`).

### Diagrama UML

![UML]

## Referências

Enquanto poucas, uma referência extremamente importante foi o *Jogo do Galo* 
feito pelo Professor Nuno Fachada em uma de suas aulas. A criação da 
enumeração em `Tilestate` e sua utilização em `Tile`, a classe `Position`, e 
a propriedade `NextTurn` na classe `Board` foram todas baseadas na lógica e
no código apresentados esta aula.

[UML]: Diagram/UML.png