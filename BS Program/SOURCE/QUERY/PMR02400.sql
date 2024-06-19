SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PMR02400_GET_REPORT')

SELECT 
    p.name AS ParameterName,
    TYPE_NAME(p.user_type_id) AS ParameterDataType,
    p.max_length AS ParameterMaxLength,
    p.precision AS ParameterPrecision,
    p.scale AS ParameterScale,
    p.is_output AS IsOutputParameter
FROM 
    sys.parameters p
INNER JOIN 
    sys.procedures sp ON p.object_id = sp.object_id
WHERE 
    sp.name = 'RSP_PMR02400_GET_REPORT'

EXEC RSP_PMR02400_GET_REPORT
''
, ''
, ''
, ''
, ''
, '1'
, ''
, ''
, ''

EXEC RSP_PMR02400_GET_REPORT
''
, ''
, ''
, ''
, ''
, '2'
, ''
, ''
, ''
