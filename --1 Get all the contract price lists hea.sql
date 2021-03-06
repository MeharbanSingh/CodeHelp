USE [E10QATemp]
GO
/****** Object:  StoredProcedure [Erp].[PbsHousingAndBuilderContractsPriceList]    Script Date: 3/02/2022 1:36:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Erp].[PbsHousingAndBuilderContractsPriceList]
	@Company NVARCHAR(10),
	@SalesRegion as NVARCHAR(50)
AS
BEGIN


-- Get the Housing and Building price list
--Drop Table IF EXISTS #ContractedPricelist
--Drop Table IF EXISTS #matrix
--Drop Table IF EXISTS #uomConv
--Drop Table IF EXISTS #pqb
--Drop Table IF EXISTS #StaginPriceLstParts
--GO

DECLARE @zero DECIMAL = 0;
Declare @CostGroup as nvarchar(8)='10';
Declare @FirstDayOfNextMonth as date = Convert(date,Dateadd(dd,1 - DATEPART(dd,getdate()), DATEADD(mm,1,getdate())),103);
Declare @LastDayOfNextMonth as date = Convert(date,Dateadd(dd,-1,Dateadd(dd,1 - DATEPART(dd,getdate()), DATEADD(mm,2,getdate()))),103);


Select Distinct  pl.Company,pl.ListCode,pl.CurrencyCode,pl.ListDescription,pl.StartDate,pl.EndDate,c.PbsBuyCodeID_c
,pl.ListType,pl.PbsPriceListRegionCode_c
INTO #ContractedPricelist 
from dbo.PriceLst pl
LEFT JOIN dbo.CustomerPriceLst CPL on CPL.Company=Pl.Company and CPL.ListCode=PL.ListCode
LEFT JOIN dbo.Customer c on c.Company=CPL.Company and c.CustNum=CPL.CustNum
where pl.ListCode like 'H%'  or pl.ListCode like 'Q%' and c.PbsBuilder_c=1 

---Get price Matrix

SELECT	    '10GAL' Company,
			cpm.ID ID,
            cpm.BuyCode BuyCode,
            cpm.GroupCode GroupCode,
            cpm.UOM UOM,
            cpm.MarkUp costPlusMarkUp,
            sm.RegionCode RegionCode,
            sm.MarkUp surchargeMarkUp,
            p.PartNum PartNum,
            p.UOMClassID UOMClassID,
            p.IUM IUM,
            p.PUM PUM,
            p.SalesUM SalesUM, 
			Case when ISNULL(ud4.Number01,@zero)<>0 then ISNULL(ud4.Number01,@zero) ELSE ISNULL(ud4_2.Number01,@zero) END as UnitPrice,                    
            p.PbsEnablePriceListQtyBreaks_c QuantityBreaks,
            p.PbsPriceListQtyBreaksUOM_c QuantityBreaksUOM,
            p.PbsPriceListQtyBreaks_c QuantityBreaksList
			INTO #matrix
	  FROM	Erp.PbsCostPlusMatrix cpm 
            LEFT OUTER JOIN Erp.PbsSurchargeMatrix sm ON sm.Company = cpm.Company AND sm.CostPlusMatrixID = cpm.ID AND sm.RegionCode = @SalesRegion
            LEFT OUTER JOIN dbo.Part p ON p.Company = cpm.Company AND p.PbsPartGroup_c = cpm.GroupCode AND p.PbsExcludeFromPriceList_c = 0 and p.InActive=0
            LEFT OUTER JOIN Erp.PbsGroupCodes pgc ON p.Company = pgc.Company AND p.PbsPartGroup_c = pgc.ID
		    LEFT OUTER JOIN Ice.UD04 ud4 on ud4.Company=p.Company and ud4.Key2=p.PartNum and  Key1=@CostGroup 
		      and Convert(date,ud4.key3,103) =(Select max(Convert(date,u.key3,103)) from ice.UD04 u where u.Company=p.Company and u.Key2=p.PartNum and CONVERT(date,u.Key3,103)<= GETDATE() and  u.Key1=@CostGroup   )
            LEFT OUTER JOIN Ice.UD04 ud4_2 on ud4_2.Company=p.Company and ud4_2.Key2=p.PartNum and  ud4_2.Key1='10' 
		      and Convert(date,ud4_2.Key3,103) =(Select max(Convert(date,ur.key3,103)) from ice.UD04 ur where ur.Company=p.Company and ur.Key2=p.PartNum and CONVERT(date,ur.Key3,103)<= GETDATE() and  ur.Key1='10'  )
			 WHERE	sm.RegionCode = @SalesRegion AND pgc.UOMClass = p.UOMClassID --and p.PartNum='TAAL0041'  --and cpm.BuyCode='26'

			 BEGIN TRANSACTION

UPDATE pl 
SET  pl.StartDate = @FirstDayOfNextMonth
    ,pl.EndDate=@LastDayOfNextMonth
from Erp.PriceLst pl
inner join #ContractedPricelist cpl on cpl.Company=pl.Company and cpl.ListCode=pl.ListCode

--Delete Non contracted Part from Housing and Building price list
DELETE PLP
from Erp.PriceLstParts PLP
inner join Erp.PriceLstParts_UD PLPUD on PLPUD.ForeignSysRowID=PLP.SysRowID
inner join #ContractedPricelist CPL on PLP.Company = CPL.Company and PLP.ListCode=CPL.ListCode
where PLPUD.PbsContractedItem_c =0 


			 -- Load UOM Conversions into mem table
	SELECT	Company = cls.Company,
			ClassId = cls.UOMClassID,
			ClassType = cls.ClassType,
			Description = cls.Description,
			BaseUom = cls.BaseUOMCode,
			Uom = cnv.UOMCode,
			Factor = cnv.ConvFactor,
			Operator = cnv.ConvOperator,
			PartSpecific = cnv.PartSpecific
	  INTO	#uomConv
	  FROM	Erp.UOMClass cls 
			LEFT OUTER JOIN Erp.UOMConv cnv ON cls.Company = cnv.Company AND cls.UOMClassID = cnv.UOMClassID
	 WHERE	cls.Active = 1
	   AND	cnv.Active = 1

	SELECT	p.PartNum
		 ,	1 [Quantity]
		 ,	0 [Percent]
	  INTO	#pqb
	  FROM	dbo.Part p
	  where p.InActive=0 and p.PbsExcludeFromPriceList_c=0

	INSERT INTO #pqb
	SELECT	p.PartNum
		 ,	CONVERT(INT, SUBSTRING(qb.V, 0, CHARINDEX('~', qb.V, 0))) 
		 ,	CONVERT(decimal, SUBSTRING(qb.V, CHARINDEX('~', qb.V, 0) + 1, LEN(qb.V) - CHARINDEX('~', qb.V, 0) + 1)) [Percent]
	  FROM	dbo.Part p
			CROSS APPLY dbo.SplitStrings(p.PbsPriceListQtyBreaks_c, ',') qb
	 WHERE	PbsEnablePriceListQtyBreaks_c = 1 
	   AND	PbsPriceListQtyBreaks_c != ''
	   and  p.InActive=0

	   
	   SELECT 	 pl.Company
	       ,pl.ListCode
		,	m.PartNum
		,	m.UOM UOMCode
		,	ISNULL(pqb.[Quantity], 1) AS [Quantity]
		,	((((
			CONVERT(decimal(18, 4), CASE 
				WHEN ISNULL(puom.ConvOperator, uomconv.Operator) = '*' THEN m.UnitPrice * ISNULL(puom.ConvFactor, uomconv.Factor)
				WHEN ISNULL(puom.ConvOperator, uomconv.Operator) = '/' THEN m.UnitPrice / ISNULL(puom.ConvFactor, uomconv.Factor)
				ELSE m.UnitPrice
			END)
			) * ( m.costPlusMarkUp)) * (1.0 + m.surchargeMarkUp)) * (1 - (ISNULL(pqb.[Percent], CONVERT(decimal, 0)) / 100))) BasePrice
		,	ISNULL(puom.ConvFactor, uomconv.Factor) Factor
		,m.BuyCode
		INTO #StaginPriceLstParts
	 FROM	#ContractedPricelist pl
			LEFT OUTER JOIN #matrix m ON pl.Company = m.Company and pl.PbsBuyCodeID_c=m.BuyCode
			LEFT OUTER JOIN #uomConv uomconv ON uomconv.ClassId = m.UOMClassID AND uomconv.Uom = m.UOM
			LEFT OUTER JOIN #pqb pqb ON pqb.PartNum = m.PartNum 
			LEFT OUTER JOIN Erp.PartUOM puom ON puom.Company = m.Company AND puom.PartNum = m.PartNum AND puom.UOMCode = m.UOM AND uomconv.PartSpecific = 1
	WHERE	pl.Company = @Company --and m.costPlusMarkUp >0
			ORDER BY m.PartNum, m.UOM, [Quantity]



Update pl set pl.BasePrice=spl.BasePrice
from Erp.PriceLstParts pl
inner join Erp.PriceLstParts_UD plpud on plpud.ForeignSysRowID=pl.SysRowID
inner join #StaginPriceLstParts spl on spl.Company=pl.Company and spl.ListCode=pl.ListCode and spl.PartNum=pl.PartNum and spl.UOMCode=pl.UOMCode
where plpud.PbsContractedItem_c=1 




	 INSERT INTO [Erp].[PriceLstParts] (
		[Company],
		[GlobalPriceLstParts], [GlobalLock],
		[ListCode],
		[PartNum],
		[BasePrice],
		[CommentText],
		[UOMCode],
		[QtyBreak1], [UnitPrice1],
		[QtyBreak2], [UnitPrice2],
		[QtyBreak3], [UnitPrice3],
		[QtyBreak4], [UnitPrice4],
		[QtyBreak5], [UnitPrice5]
	)
	SELECT	spl.[Company]
		 ,	0, 0
		 ,	spl.[ListCode]
		 ,	spl.[PartNum]
		 ,	spl.[BasePrice]
		 ,	''
		 ,	spl.[UOMCode]
		 ,	 0,0
		 ,	 0,0
		 ,	 0,0
		 ,	 0,0
		 ,   0,0
	  FROM	#StaginPriceLstParts spl
	  WHERE NOT EXISTS (Select plp.Company,plp.ListCode,plp.PartNum,plp.UOMCode from Erp.PriceLstParts plp where plp.Company=spl.Company and plp.ListCode= spl.ListCode and plp.PartNum = spl.PartNum and plp.UOMCode=spl.UOMCode)
 
		

	 INSERT INTO [Erp].[PriceLstParts_UD] (
	      [ForeignSysRowID],
		  [PbsContractedItem_c]
                             )
	 SELECT plp.SysRowID,
	        0
	 FROM Erp.PriceLstParts plp 
	 where NOT EXISTS (Select * FROM Erp.PriceLstParts_UD plpUD where plpUD.ForeignSysRowID=plp.SysRowID)

    COMMIT TRANSACTION
  END
