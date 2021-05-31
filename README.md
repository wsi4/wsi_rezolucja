# Resolution

## Usage

The project is a console application that takes two arguments. The first one is the path to the Diseases description file, while the second one is the paath to the patients description file. Both files are described more thoroughly in below sections.

Sample invocation of the program:
```
> Launcher.exe .\exampleSet.dis .\patients.pat
```

## Patients description file

The file containing the set of patients and their symptoms needs to contain at least one line defined according to the schema below:
```
<name> | <symptom_1>, <symptom_2> | <not_symptom_1>, <not_symptom_2> | <disease>
```
where `<name>` is the name of the patient, `<symptom_1>, <symptom_2>` is the coma-delimited list of symtoms the patient exhibits, `<not_symptom_1>, <not_symptom_2>` is the list of symptoms the patient does NOT exhibit, and `<disease>` is the diagnosis that should be verified. Every section is delimited with the pipe (`|`) symbol. Sample file, called `patients.pat` can be closer examined in the Launcher project.

## Disease description file
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

Sample file, called `exampleSet.dis` can be closer examined in the Launcher project.
### Grammar of the disease description
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

### Example
```
Choroby
{
    (a);
    <(a) or (b)>;
    <<(a) and (b) and (c)> or <(d) imp (e)>>;
}
```
