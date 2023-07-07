grammar CarlitosLang;

program: line* EOF;

line: statement | ifBlock | whileBlock;

statement: (assignment|functioncall) ';';

ifBlock: 'if' expression block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: WHILE expression  block ('else' elseIfBlock)?;

WHILE: 'while' | 'untill';

assignment: ID '=' expression;

functioncall: ID '(' (expression (',' expression)*)? ')';

expression
    : constant                             #constantExpression
    | ID                                   #IDExpression
    | functioncall                         #functioncallExpression
    | '(' expression ')'                   #parenthesizedExpression
    | '!' expression                       #notExpression
    | expression multOp expression         #multiplicativeExpression
    | expression addOp expression          #additiveExpression
    | expression compareOp expression      #comparisonExpression
    | expression boolOp expression         #booleanExpression
    ;

multOp: '*' | '/' | '%';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: 'and' | 'or' |'xor';


constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: [0-9]+;
FLOAT: [0-9]+ '.' [0-9]+;
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\'');
BOOL: 'true' | 'false';
NULL: 'null';

block: '{' line* '}';

WS: [ \t\r\n]+ -> skip;

ID: [a-zA-Z_][a-zA-Z0-9_]*;