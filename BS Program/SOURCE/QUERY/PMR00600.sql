EXEC RSP_PMR00600_GET_REPORT 'RCD'
, 'JBMPC'
, '202401'
, '202412'
, ''
, ''
, ''
, ''
, '1'
, ''
, ''
, ''
, ''
, ''
, ''
, 'EN'

SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PMR00600_GET_REPORT')