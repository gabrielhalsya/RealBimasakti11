SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PM_SAVE_STAMP_RATE')

EXEC sp_help 'PMM_STAMP_RATE';
delete from sat_locking where CUSER_ID= 'ghc' 

EXEC RSP_GS_GET_CURRENCY_LIST 'rcd','ghc'
EXEC RSP_GS_GET_PROPERTY_LIST 'rcd','ghc'

EXEC RSP_PM_GET_STAMP_RATE_LIST 'rcd','ASHMD','en'
EXEC RSP_PM_GET_STAMP_RATE_DATE_LIST 'rcd','ASHMD','C411A72A-4043-44BB-B2ED-BAC30E06EE20','en'
EXEC RSP_PM_GET_STAMP_RATE_AMOUNT_LIST 'rcd','ASHMD','A51D3789-D9DC-430A-85C9-D4ADA07408E2','A77CAEAD-F140-4A23-86E5-169AA94476E8','en'

EXEC RSP_PM_GET_STAMP_RATE 'rcd','','','A77CAEAD-F140-4A23-86E5-169AA94476E8','en'
EXEC RSP_PM_GET_STAMP_RATE_DATE 'rcd','A51D3789-D9DC-430A-85C9-D4ADA07408E2','en'
EXEC RSP_PM_GET_STAMP_RATE_AMOUNT 'rcd','832977E7-334C-4F77-A05C-6D3A276EEA3B','en'


SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PM_SAVE_STAMP_RATE')

EXEC RSP_PM_DELETE_STAMP_RATE 'rcd','ASHMD','stampcode','(stampcode)crecid'
EXEC RSP_PM_DELETE_STAMP_RATE_DATE '(stampcode)crecid'
EXEC RSP_PM_DELETE_STAMP_RATE_AMOUNT '(stampamount)crecid'

EXEC RSP_PM_SAVE_STAMP_RATE 'rcd','ASHMD','ghc','ADD','','','desc','curcode'
EXEC RSP_PM_SAVE_STAMP_RATE_DATE 'rcd','ASHMD','ghc','ADD','stampcode','(stampcode)crecid','stampdateid','yyyymmdd'
EXEC RSP_PM_SAVE_STAMP_RATE_AMOUNT 'rcd','ASHMD','ghc','ADD','cstampcode','(stampcode)crecid','(stampdate)crecid','(stampamount)crecid','yyyymmdd',2000,2000


 