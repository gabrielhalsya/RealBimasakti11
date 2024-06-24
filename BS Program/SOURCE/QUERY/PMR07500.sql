SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PM_SAVE_STAMP_RATE')

EXEC RSP_GS_GET_COMPANY_INFO 'rcd'
EXEC RSP_GL_GET_SYSTEM_PARAM 'rcd','en'
EXEC RSP_CB_GET_SYSTEM_PARAM  'rcd','en'
EXEC RSP_GS_GET_PERIOD_DT_INFO 'rcd','2021','07'
EXEC RSP_GS_GET_DEPT_LOOKUP_LIST 'rcd','ghc',''
EXEC RSP_GS_GET_TRANS_CODE_INFO 'rcd',''
EXEC RSP_GS_GET_PERIOD_YEAR_RANGE 'rcd','',''

EXEC RSP_PM_GET_STAMP_RATE_LIST 'rcd','ASHMD','en'
EXEC RSP_PM_GET_STAMP_RATE_DATE_LIST 'rcd','ASHMD','6470CFFA-2243-410A-B71B-86E603C77F86','en'
EXEC RSP_PM_GET_STAMP_RATE_AMOUNT_LIST 'rcd','ASHMD','(rate)CREC_ID','(ratedate)CREC_ID','en'

EXEC RSP_PM_GET_STAMP_RATE 'rcd','','','','en'
EXEC RSP_PM_GET_STAMP_RATE_DATE 'rcd','','en'
EXEC RSP_PM_GET_STAMP_RATE_AMOUNT 'rcd','','en'

EXEC RSP_PM_DELETE_STAMP_RATE 'rcd','ASHMD','stampcode','(stampcode)crecid'
EXEC RSP_PM_DELETE_STAMP_RATE_DATE '(stampcode)crecid'
EXEC RSP_PM_DELETE_STAMP_RATE_AMOUNT '(stampamount)crecid'

EXEC RSP_PM_SAVE_STAMP_RATE 'rcd','ASHMD','ghc','ADD','','','desc','curcode'
EXEC RSP_PM_SAVE_STAMP_RATE_DATE 'rcd','ashmd','ghc','ADD','stampcode','(stampcode)crecid','stampdateid','yyyymmdd'
EXEC RSP_PM_SAVE_STAMP_RATE_AMOUNT 'rcd','ASHMD','ghc','ADD','cstampcode','(stampcode)crecid','(stampdate)crecid','(stampamount)crecid','yyyymmdd',2000,2000


 