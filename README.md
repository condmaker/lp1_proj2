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

Idem à `Tilestate`.

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

#### Classe `Board`

Todas as variáveis e propriedades foram criadas por Daniel Fernandes. Os únicos
métodos que não foram criados por Daniel Fernandes foram `PlacePieces()`, 
`UpdateSimple()` e `UpdateEnemy()`.

#### Classe `Tile`

Todas as variáveis e propriedades foram criadas por Daniel Fernandes. O único
método não criado por Daniel Fernandes foi `IsSurronded()`.

#### Classe `Game`

Enquanto Daniel Fernandes não contribuiu diretamente para esta classe, a 
retirada de pequenas dúvidas sobre certas funcionalidades das outras 
(nomeadamente `Board` e `Tile`) para implementação no *game loop* de 
`BeginGame()` (como, por exemplo, a funcionalidade da propriedade de turnos).

#### Classe `Position`

Tudo, excepto a documentação, foi feita por Daniel Fernandes.

#### Diagrama UML

Feito completamente por Daniel Fernandes.

**[Link para o repositório *Github*](https://github.com/condmaker/lp1_proj2)**

## Arquitetura da Solução

## Referências

Enquanto poucas, uma referência extremamente importante foi o *Jogo do Galo* 
feito pelo Professor Nuno Fachada em uma de suas aulas. A criação da 
enumeração em `Tilestate` e sua utilização em `Tile`, a classe `Position`, e 
a propriedade `NextTurn` na classe `Board` foram todas baseadas na lógica e
no código apresentados esta aula.
