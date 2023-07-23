<a name="problem-description"></a><a name="binary-linear-programming-approach"></a>Требуется разбить предложенный массив из весов рулонов на машины двух типов (22.2 и 27.6, превышать эту нагрузку нельзя), таким образом, чтобы удельная загрузка была максимальной и все рулоны были разложены по машинам. 

Под удельной загрузкой понимается отношение общего веса всех рулонов к предельной нагрузке всех используемых машин. 

*Введём необходимые обозначения*:

n – количество рулонов;

m – количество доступных машин;

W=w1,...,wn – веса рулонов;

C=c1,...,cm – максимальные нагрузки доступных машин и ci∈22.2, 27.6 для i=1,m.

*Введём необходимые переменные*:

xi,j* – бинарная переменная, которой присваивается значение 1, если рулон i загружен в машину j, и значение 0 в противном случае;

yj* – бинарная переменная, которой присваивается значение 1, если машина j загружена хотя бы одним рулоном, и значение 0 в противном случае.

*Сформулируем модель двоичного линейного программирования.*

Минимизировать 

|j=1mcj∙yj|(1)|
| - | :-: |

при ограничениях:

|j=1mxi,j=1,     i=1,n;|(2)|
| - | :-: |
|i=1nwi∙xi,j≤cj∙yj,     j=1,m;|(3)|
|xi,j, yj∈0,1,     j=1,n, j=1,m;|(4)|

Целевая функция (1) максимизирует удельную загрузку. В постановке задачи требуется максимизировать целевую функцию i=1nwij=1mcj∙yj, но так как числитель данной функции является константой, для ее максимизации достаточно минимизировать сумму j=1mcj∙yj. 

Ограничения (2) гарантируют, что каждый рулон размещается ровно в одной машине. Ограничения (3) гарантируют, что загрузка каждой используемой машины не превышает ее предельную нагрузку. Ограничения (4) устанавливают, что все переменные имеют бинарный характер.


Решение задачи разбивается на 3 этапа:

1. Находится минимальное количество машин первого типа (minNumTrucksT1) в случае, когда рулоны загружаются только в машины первого типа. Для нахождения решения используется вышеописанная модель, в которой количество доступных машин первого типа равно 
   m=n/22.2/maxW, и не используются машины второго типа.
1. Находится минимальное количество машин второго типа (minNumTrucksT2) в случае, когда рулоны загружаются только в машины второго типа. Для нахождения решения используется вышеописанная модель, в которой количество доступных машин второго типа равно 
   m=n/27.6/maxW, и не используются машины первого типа (22.2).
1. Находится оптимальное решение поставленной задачи. В этом случае рулоны могут загружаться в машины первого и второго типов. Для нахождения решения используется вышеописанная модель, в которой количество доступных машин первого и второго типов равно minNumTrucksT1 и minNumTrucksT2 соответственно.

`     `Время решения всех трёх этапов не превышает 55 секунд на процессоре AMD Ryzen 5 5600U на 10 потоках. Результат выполнения программы на Python с использованием солвера Gurobi:

Set parameter MIPGap to value 0.001

Set parameter TimeLimit to value 150

Set parameter Threads to value 10

Gurobi Optimizer version 10.0.1 build v10.0.1rc0 (win64)

CPU model: AMD Ryzen 5 5600U with Radeon Graphics, instruction set [SSE2|AVX|AVX2]

Thread count: 6 physical cores, 12 logical processors, using up to 10 threads

Optimize a model with 86 rows, 1682 columns and 3335 nonzeros

Model fingerprint: 0x14d48695

Variable types: 0 continuous, 1682 integer (1682 binary)

Coefficient statistics:

`  `Matrix range     [1e+00, 2e+01]

`  `Objective range  [2e+01, 2e+01]

`  `Bounds range     [1e+00, 1e+00]

`  `RHS range        [1e+00, 1e+00]

Found heuristic solution: objective 621.6000000

Presolve time: 0.00s

Presolved: 86 rows, 1682 columns, 3335 nonzeros

Variable types: 0 continuous, 1682 integer (1682 binary)

Root relaxation: objective 4.441800e+02, 167 iterations, 0.00 seconds (0.00 work units)

`    `Nodes    |    Current Node    |     Objective Bounds      |     Work

` `Expl Unexpl |  Obj  Depth IntInf | Incumbent    BestBd   Gap | It/Node Time

`     `0     0  444.18000    0   39  621.60000  444.18000  28.5%     -    0s

H    0     0                     577.2000000  444.18000  23.0%     -    0s

H    0     0                     555.0000000  444.18000  20.0%     -    0s

H    0     0                     532.8000000  444.18000  16.6%     -    0s

`     `0     0  444.18000    0   49  532.80000  444.18000  16.6%     -    0s

H    0     0                     510.6000000  444.18000  13.0%     -    0s

`     `0     0  444.18000    0   59  510.60000  444.18000  13.0%     -    0s

`     `0     0  444.18000    0   39  510.60000  444.18000  13.0%     -    0s

`     `0     0  444.18000    0   38  510.60000  444.18000  13.0%     -    0s

`     `0     2  444.18000    0   38  510.60000  444.18000  13.0%     -    0s

Cutting planes:

`  `Gomory: 6

`  `Cover: 9

`  `Clique: 1

`  `MIR: 83

`  `StrongCG: 169

`  `Flow cover: 37

`  `Inf proof: 3

Explored 3207 nodes (110299 simplex iterations) in 4.60 seconds (4.14 work units)

Thread count was 10 (of 12 available processors)

Solution count 5: 510.6 532.8 555 ... 621.6

Optimal solution found (tolerance 1.00e-03)

Best objective 5.106000000000e+02, best bound 5.106000000000e+02, gap 0.0000%

Set parameter MIPGap to value 0.001

Set parameter TimeLimit to value 150

Set parameter Threads to value 10

Gurobi Optimizer version 10.0.1 build v10.0.1rc0 (win64)

CPU model: AMD Ryzen 5 5600U with Radeon Graphics, instruction set [SSE2|AVX|AVX2]

Thread count: 6 physical cores, 12 logical processors, using up to 10 threads

Optimize a model with 76 rows, 1102 columns and 2185 nonzeros

Model fingerprint: 0x9dcd435d

Variable types: 0 continuous, 1102 integer (1102 binary)

Coefficient statistics:

`  `Matrix range     [1e+00, 3e+01]

`  `Objective range  [3e+01, 3e+01]

`  `Bounds range     [1e+00, 1e+00]

`  `RHS range        [1e+00, 1e+00]

Found heuristic solution: objective 524.4000000

Presolve time: 0.00s

Presolved: 76 rows, 1102 columns, 2185 nonzeros

Variable types: 0 continuous, 1102 integer (1102 binary)

Root relaxation: objective 4.441800e+02, 154 iterations, 0.00 seconds (0.00 work units)

`    `Nodes    |    Current Node    |     Objective Bounds      |     Work

` `Expl Unexpl |  Obj  Depth IntInf | Incumbent    BestBd   Gap | It/Node Time

`     `0     0  444.18000    0   23  524.40000  444.18000  15.3%     -    0s

H    0     0                     496.8000000  444.18000  10.6%     -    0s

`     `0     0  444.18000    0   50  496.80000  444.18000  10.6%     -    0s

`     `0     0  444.18000    0   52  496.80000  444.18000  10.6%     -    0s

`     `0     0  444.18000    0   41  496.80000  444.18000  10.6%     -    0s

`     `0     0  444.18000    0   31  496.80000  444.18000  10.6%     -    0s

`     `0     2  444.18000    0   28  496.80000  444.18000  10.6%     -    0s

H  117    69                     469.2000000  469.20000  0.00%  18.8    0s

Cutting planes:

`  `Cover: 44

`  `Clique: 3

`  `MIR: 28

`  `StrongCG: 9

Explored 128 nodes (4482 simplex iterations) in 0.45 seconds (0.36 work units)

Thread count was 10 (of 12 available processors)

Solution count 3: 469.2 496.8 524.4 

Optimal solution found (tolerance 1.00e-03)

Best objective 4.692000000000e+02, best bound 4.692000000000e+02, gap 0.0000%

Set parameter MIPGap to value 0.001

Set parameter TimeLimit to value 150

Set parameter Threads to value 10

Gurobi Optimizer version 10.0.1 build v10.0.1rc0 (win64)

CPU model: AMD Ryzen 5 5600U with Radeon Graphics, instruction set [SSE2|AVX|AVX2]

Thread count: 6 physical cores, 12 logical processors, using up to 10 threads

Optimize a model with 97 rows, 2320 columns and 4600 nonzeros

Model fingerprint: 0x48635b5f

Variable types: 0 continuous, 2320 integer (2320 binary)

Coefficient statistics:

`  `Matrix range     [1e+00, 3e+01]

`  `Objective range  [2e+01, 3e+01]

`  `Bounds range     [1e+00, 1e+00]

`  `RHS range        [1e+00, 1e+00]

Found heuristic solution: objective 775.2000000

Presolve time: 0.01s

Presolved: 97 rows, 2320 columns, 4600 nonzeros

Variable types: 0 continuous, 2320 integer (2320 binary)

Root relaxation: objective 4.441800e+02, 233 iterations, 0.00 seconds (0.00 work units)

`    `Nodes    |    Current Node    |     Objective Bounds      |     Work

` `Expl Unexpl |  Obj  Depth IntInf | Incumbent    BestBd   Gap | It/Node Time

`     `0     0  444.18000    0   29  775.20000  444.18000  42.7%     -    0s

H    0     0                     570.0000000  444.18000  22.1%     -    0s

H    0     0                     542.4000000  444.18000  18.1%     -    0s

`     `0     0  444.18000    0   48  542.40000  444.18000  18.1%     -    0s

H    0     0                     498.0000000  444.18000  10.8%     -    0s

H    0     0                     486.6000000  444.18000  8.72%     -    0s

H    0     0                     469.8000000  444.18000  5.45%     -    0s

`     `0     0  444.18000    0   44  469.80000  444.18000  5.45%     -    0s

`     `0     0  444.18000    0   38  469.80000  444.18000  5.45%     -    0s

`     `0     0  444.18000    0   57  469.80000  444.18000  5.45%     -    0s

`     `0     0  444.18000    0   30  469.80000  444.18000  5.45%     -    0s

`     `0     0  444.18000    0   28  469.80000  444.18000  5.45%     -    0s

`     `0     2  444.18000    0   25  469.80000  444.18000  5.45%     -    0s

H   39    14                     464.4000000  444.18000  4.35%  43.0    0s

`  `1677  1064  453.39449   89   62  464.40000  453.39449  2.37%  25.3    5s

`  `3447  1968  453.57132   67   66  464.40000  453.56786  2.33%  40.7   10s

` `12767  9087  454.20000   81   60  464.40000  453.56786  2.33%  44.9   15s

` `19223 13751  454.20000  155   49  464.40000  453.56786  2.33%  48.7   20s

` `30349 22212  454.20000   99   63  464.40000  453.56786  2.33%  50.6   25s

` `32457 24508     cutoff  208       464.40000  453.56786  2.33%  52.0   30s

` `34326 24517  454.57192   90   44  464.40000  453.57876  2.33%  52.2   35s

` `34528 24600  460.13955   68   48  464.40000  453.57876  2.33%  53.3   41s

H34536 23370                     459.0000000  453.57876  1.18%  53.3   41s

` `35764 22957  457.03698   84   58  459.00000  455.39391  0.79%  54.4   45s

Cutting planes:

`  `Gomory: 16

`  `Cover: 21

`  `Clique: 3

`  `MIR: 158

`  `StrongCG: 112

`  `Flow cover: 115

`  `Inf proof: 4

`  `Zero half: 5

Explored 36534 nodes (1987854 simplex iterations) in 46.25 seconds (46.25 work units)

Thread count was 10 (of 12 available processors)

Solution count 8: 459 464.4 469.8 ... 775.2

Optimal solution found (tolerance 1.00e-03)

Best objective 4.590000000000e+02, best bound 4.590000000000e+02, gap 0.0000%

The optimal specific load (максимальная удельная загрузка):  0.9677124183006532

Load of trucks (распределение рулонов по машинам):

Truck 1 (22.2) is loaded with rolls: [36, 37, 54] with weights: [8.26, 8.2, 5.74].

Truck 2 (22.2) is loaded with rolls: [32, 41, 47] with weights: [8.31, 7.85, 6.03].

Truck 3 (22.2) is loaded with rolls: [31, 39, 52] with weights: [8.31, 8.12, 5.76].

Truck 4 (22.2) is loaded with rolls: [33, 35, 55] with weights: [8.28, 8.27, 5.64].

Truck 5 (22.2) is loaded with rolls: [22, 30, 56] with weights: [8.49, 8.33, 5.31].

Truck 6 (22.2) is loaded with rolls: [34, 38, 50] with weights: [8.27, 8.15, 5.78].

Truck 7 (22.2) is loaded with rolls: [17, 44, 46] with weights: [8.57, 7.06, 6.3].

Truck 8 (27.6) is loaded with rolls: [3, 11, 19] with weights: [8.77, 8.62, 8.53].

Truck 9 (27.6) is loaded with rolls: [1, 4, 10] with weights: [8.78, 8.76, 8.63].

Truck 10 (27.6) is loaded with rolls: [14, 15, 16] with weights: [8.61, 8.6, 8.57].

Truck 11 (27.6) is loaded with rolls: [24, 26, 29] with weights: [8.44, 8.41, 8.37].

Truck 12 (27.6) is loaded with rolls: [28, 43, 48, 49] with weights: [8.38, 7.13, 6.02, 5.81].

Truck 13 (27.6) is loaded with rolls: [25, 40, 53, 57] with weights: [8.42, 8.12, 5.75, 5.31].

Truck 14 (27.6) is loaded with rolls: [2, 6, 13] with weights: [8.77, 8.73, 8.62].

Truck 15 (27.6) is loaded with rolls: [5, 7, 8] with weights: [8.74, 8.72, 8.7].

Truck 16 (27.6) is loaded with rolls: [9, 12, 20] with weights: [8.63, 8.62, 8.5].

Truck 17 (27.6) is loaded with rolls: [18, 21, 23] with weights: [8.53, 8.49, 8.47].

Truck 18 (27.6) is loaded with rolls: [27, 42, 45, 51] with weights: [8.39, 7.14, 6.31, 5.76].

​








