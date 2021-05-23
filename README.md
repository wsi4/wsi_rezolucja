# Resolution

## Parsing and logic input language
Class library includes parser for basic logic language that was defined. To retrieve parsed knowledge base user need to invoke
***FileReader.ReadFileX( absoluteFilePath)***. It will returns
***IEnumarable\<Sentence>***. In case of error it will throw ***ParsingException***.

### Language definition
Every file with set of diseases need to be defined as following
```
Choroby
{
    expression_1;
    .
    .
    .
    expression_n;
}
```
Where expression follow rules:
1. Literal indetifier have to be between *()* and contains at least one not white character. Example *(symbol1)*
2. Expression can contains 4 non-unary logic operators
    ***and, or, imp, bicon*** which means logic and, or, implication, biconditional.
3. Expression can contain negation operator ***not(symbol1)***, which need to be followed by literal symbol. 
4. There cannot be 2 literals or connectives in a row.
5. It cannot start or end with non-unary operator.
6. Expression can contains nested expression ***<expression_nested>***.

### Grammar of language
```
G= (V,T,P, START)
V = {START, EXPS, EXP, LIT, CONN, SUB}
T = {'Choroby', '{', '}', '<', '>', ';', '([string])', 'and', 'or', 'not', 'imp', 'bicon' }

EPS <=> epsilon

P:
START => Choroby { EXPS } | EPS
EXPS => EXP ; EXP | EXP ;
EXP => LIT CONN EXP | LIT | SUB | SUB CONN EXP
SUB => <EXP>
LIT => ([string]) | not([string])
CONN => and | or | imp | bicon
```

