# Resolution

## Parsing the logic input language
Class library includes a parser for a simple logical language. To instantiate the given knowledge base, user needs to invoke
```FileReader.ReadFileX(absoluteFilePath)```. The method returns
```IEnumerable<Sentence>```. In case of error, the method throws a ```ParsingException```.

### Language definition
Every file with set of diseases needs to be defined according to the schema below:
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
Expressions adhere to the following rules:
1. literals must be wrapped in parentheses and contain at least one non-blank character, e.g.: *(symbol1)*,
2. expressions can contain 4 non-unary logic operators
    `and`, `or`, `imp`, `bicon` which symbolize logical connectives *and*, *or*, *implication*, and *biconditional*,
3. expressions can contain a negation operator `not`, which needs to be followed by a sentence, e.g.:  `not(symbol1)`,
4. there cannot be 2 sentences or connectives in a row,
5. expressions cannot start or end with a non-unary operator,
6. sentences can be nested using brackets, e.g.: `<<[sentence] [connective] [sentence]> [connective] [sentence]>`.

## Grammar of language
```
G = (V,T,P, START)
V = {START, EXPS, EXP, LIT, CONN, SUB}
T = {'Choroby', '{', '}', '<', '>', ';', '([string])', 'and', 'or', 'not', 'imp', 'bicon' }

EPS <=> epsilon

P:
START => Choroby { EXPS } | EPS
EXPS => EXP ; EXPS | EXP ;
EXP => LIT CONN EXP | LIT | SUB | SUB CONN EXP
SUB => <EXP>
LIT => ([string]) | not([string])
CONN => and | or | imp | bicon
```

## Example
```
Choroby
{
    (a);
    <(a) or (b)>;
    <<(a) and (b) and (c)> or <(d) imp (e)>>;
}
```
