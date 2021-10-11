USE [CoreCard ]
GO
/****** Object:  StoredProcedure [dbo].[SVCategory]    Script Date: 11-10-2021 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SVCategory]
@Action nvarchar(50) = null,
@CategoryId int = null,
@CategoryName nvarchar(100) = null
AS
Begin

if(@Action = 'GelAll')
Begin
    SELECT * FROM tblMstCategory where IsDeleted = 0 order by CategoryName asc
 -- if(@CategoryId !=0)
 --  begin
	--SELECT * FROM tblMstCategory where CategoryId=@CategoryId and  IsDeleted = 0
 --  end
 -- else 
	--begin 
	-- SELECT * FROM tblMstCategory where CategoryName=@CategoryName and  IsDeleted = 0
	--end
End

if(@Action = 'GelById')
Begin
  SELECT * FROM tblMstCategory where CategoryId=@CategoryId and  IsDeleted = 0
End

if(@Action = 'Insert')
Begin
  INSERT INTO tblMstCategory (CategoryName) VALUES(@CategoryName)
END

if(@Action = 'Update')
Begin  
   UPDATE tblMstCategory SET CategoryName=@CategoryName where CategoryId=@CategoryId
END

if(@Action = 'Delete')
Begin
   UPDATE tblMstCategory SET IsDeleted=1, DeletedDate=GETDATE() where CategoryId=@CategoryId
END

End

GO
/****** Object:  StoredProcedure [dbo].[SVExistingProcedure]    Script Date: 11-10-2021 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SVExistingProcedure]
@Action nvarchar(50) = null,
@ExistingProcedureId int = null,
@ExistingProcedureName nvarchar(500) = null
AS
Begin

if(@Action = 'GelAll')
Begin
   select * from tblMstExistingProcedure where IsDeleted = 0 order by ExistingProcedureName asc
END

if(@Action = 'GelById')
Begin
  -- select * from tblMstExistingProcedure where ExistingProcedureId = @ExistingProcedureId and IsDeleted = 0
	if(@ExistingProcedureId !=0)
		SELECT * FROM tblMstExistingProcedure where ExistingProcedureId=@ExistingProcedureId and IsDeleted = 0
	else 
	begin 
		SELECT * FROM tblMstExistingProcedure where ExistingProcedureName=@ExistingProcedureName and IsDeleted = 0
	end
END

if(@Action = 'Insert')
Begin
  INSERT INTO tblMstExistingProcedure (ExistingProcedureName) VALUES(@ExistingProcedureName)
END

if(@Action = 'Update')
Begin
   UPDATE tblMstExistingProcedure SET ExistingProcedureName=@ExistingProcedureName where ExistingProcedureId=@ExistingProcedureId
END

if(@Action = 'Delete')
Begin
   UPDATE tblMstExistingProcedure SET IsDeleted=1, DeletedDate=GETDATE() where ExistingProcedureId=@ExistingProcedureId
END

End
GO
/****** Object:  StoredProcedure [dbo].[SVValidationSteps]    Script Date: 11-10-2021 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SVValidationSteps]
@Action nvarchar(50) = null,
@TaskActivityId int = null,
@GroupId int = null,
@TaskActivityName nvarchar(max) = null,
@CategoryId int = null,
@ComplexietyId int = null,
@ExistingProcedureId int = null,
@FrequencyId int = null
AS
Begin

if(@Action = 'GetCategoryList')
Begin
	select * from tblMstCategory where IsDeleted = 0 order by CategoryName asc
End
if(@Action = 'GetComplexietyList')
Begin
	select * from tblMstComplexiety where IsDeleted = 0 order by ComplexietyName asc
End
if(@Action = 'GetExistingProcedureList')
Begin
	select * from tblMstExistingProcedure where IsDeleted = 0 order by ExistingProcedureName asc
End
if(@Action = 'GetFrequencyList')
Begin
	select * from tblMstFrequency where IsDeleted = 0 order by FrequencyName asc
End

if(@Action = 'GelAll')
Begin
   --select * from tblMstValidationSteps where IsDeleted = 0
   select Vst.GroupId,				Grp.GroupName,					Vst.TaskActivityId,   Vst.TaskActivityName,
          Vst.CategoryId ,			Cat.CategoryName,				 Vst.ComplexietyId,    Comp.ComplexietyName, 
          Vst.ExistingProcedureId,	ExProd.ExistingProcedureName,		 Vst.FrequencyId,      Fre.FrequencyName
   from tblMstValidationSteps as Vst inner join tblMstGroup as Grp on Vst.GroupId = Grp.GroupId   
   inner join tblMstCategory as Cat on Cat.CategoryId = Vst.CategoryId 
   inner join tblMstComplexiety as Comp on Comp.ComplexietyId = Vst.ComplexietyId 
   inner join tblMstExistingProcedure ExProd on ExProd.ExistingProcedureId = Vst.ExistingProcedureId 
   inner join tblMstFrequency Fre  on Fre.FrequencyId = Vst.FrequencyId 
   where Vst.IsDeleted = 0 order by   Vst.TaskActivityName asc

END

if(@Action = 'GelById')
Begin
  -- select * from tblMstExistingProcedure where ExistingProcedureId = @ExistingProcedureId and IsDeleted = 0
	if(@ExistingProcedureId !=0)
		select Vst.GroupId,				Grp.GroupName,					Vst.TaskActivityId,   Vst.TaskActivityName,
          Vst.CategoryId ,			Cat.CategoryName,				 Vst.ComplexietyId,    Comp.ComplexietyName, 
          Vst.ExistingProcedureId,	ExProd.ExistingProcedureName,		 Vst.FrequencyId,      Fre.FrequencyName
   from tblMstValidationSteps as Vst inner join tblMstGroup as Grp on Vst.GroupId = Grp.GroupId   
   inner join tblMstCategory as Cat on Cat.CategoryId = Vst.CategoryId 
   inner join tblMstComplexiety as Comp on Comp.ComplexietyId = Vst.ComplexietyId 
   inner join tblMstExistingProcedure ExProd on ExProd.ExistingProcedureId = Vst.ExistingProcedureId 
   inner join tblMstFrequency Fre  on Fre.FrequencyId = Vst.FrequencyId 
   where Vst.IsDeleted = 0 and Vst.TaskActivityId=@TaskActivityId
	else 
	begin 
		SELECT * FROM tblMstValidationSteps where TaskActivityId=@TaskActivityId and IsDeleted = 0
	end
END

if(@Action = 'Insert')
Begin
  INSERT INTO tblMstValidationSteps (GroupId,TaskActivityName,CategoryId,ComplexietyId,ExistingProcedureId,FrequencyId ) VALUES(@GroupId,@TaskActivityName,@CategoryId,@ComplexietyId,@ExistingProcedureId,@FrequencyId)
END

if(@Action = 'Update')
Begin
   UPDATE tblMstValidationSteps SET TaskActivityName=@TaskActivityName,CategoryId =@CategoryId , ComplexietyId=@ComplexietyId,ExistingProcedureId=@ExistingProcedureId
   where TaskActivityId=@TaskActivityId
   --   UPDATE tblMstValidationSteps SET TaskActivityName='aaaaaa' where TaskActivityId=3
   -- select * from tblMstValidationSteps  
END

if(@Action = 'Delete')
Begin
   UPDATE tblMstValidationSteps SET IsDeleted=1, DeletedDate=GETDATE() where  TaskActivityId=@TaskActivityId
END

End
GO
/****** Object:  StoredProcedure [dbo].[SVValidationStepsTracking]    Script Date: 11-10-2021 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SVValidationStepsTracking]
@Action nvarchar(max) = null,
@TaskActivityId int = null,
@GroupId int = null,
@TaskActivityName nvarchar(max) = null,
@CategoryId int = null,
@ComplexietyId int = null,
@ExistingProcedureId int = null,
@FrequencyId int = null
AS
Begin

if(@Action = 'GetValidationSteps')
Begin
   --select * from tblMstValidationSteps where IsDeleted = 0
   select Vst.GroupId,				Grp.GroupName,					Vst.TaskActivityId,   Vst.TaskActivityName,
          Vst.CategoryId ,			Cat.CategoryName,				 Vst.ComplexietyId,    Comp.ComplexietyName, 
          Vst.ExistingProcedureId,	ExProd.ExistingProcedureName,		 Vst.FrequencyId,      Fre.FrequencyName
   from tblMstValidationSteps as Vst inner join tblMstGroup as Grp on Vst.GroupId = Grp.GroupId   
   inner join tblMstCategory as Cat on Cat.CategoryId = Vst.CategoryId 
   inner join tblMstComplexiety as Comp on Comp.ComplexietyId = Vst.ComplexietyId 
   inner join tblMstExistingProcedure ExProd on ExProd.ExistingProcedureId = Vst.ExistingProcedureId 
   inner join tblMstFrequency Fre  on Fre.FrequencyId = Vst.FrequencyId 
   where Vst.IsDeleted = 0 order by   Vst.TaskActivityName asc
END

if(@Action = 'GetValidationScript_ByValidationStepsID')
Begin
	select * from tblMstValitationScripts where TaskActivityId = @TaskActivityId and  IsDeleted = 0 
End

End

GO
/****** Object:  StoredProcedure [dbo].[SVValitationScripts ]    Script Date: 11-10-2021 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SVValitationScripts ]
@Action nvarchar(50) = null,
@ScriptId int = null,
@ScriptName nvarchar(max) = null,
@ScriptPath nvarchar(max) = null,
@TaskActivityId int = null
AS
Begin

if(@Action = 'GetValidationStepsList')
Begin
	--select * from tblMstValidationSteps where IsDeleted = 0 order by TaskActivityName asc
	select Vst.GroupId,				Grp.GroupName,					Vst.TaskActivityId,   Vst.TaskActivityName,
          Vst.CategoryId ,			Cat.CategoryName,				 Vst.ComplexietyId,    Comp.ComplexietyName, 
          Vst.ExistingProcedureId,	ExProd.ExistingProcedureName,		 Vst.FrequencyId,      Fre.FrequencyName
   from tblMstValidationSteps as Vst inner join tblMstGroup as Grp on Vst.GroupId = Grp.GroupId   
   inner join tblMstCategory as Cat on Cat.CategoryId = Vst.CategoryId 
   inner join tblMstComplexiety as Comp on Comp.ComplexietyId = Vst.ComplexietyId 
   inner join tblMstExistingProcedure ExProd on ExProd.ExistingProcedureId = Vst.ExistingProcedureId 
   inner join tblMstFrequency Fre  on Fre.FrequencyId = Vst.FrequencyId 
   where Vst.IsDeleted = 0 order by   Vst.TaskActivityName asc
End

if(@Action = 'GelAll')
Begin  
   select ValScr.ScriptId,				ValScr.ScriptName, ValScr.ScriptPath,	ValStep.TaskActivityId,   ValStep.TaskActivityName        
   from tblMstValitationScripts as ValScr inner join tblMstValidationSteps as ValStep on ValScr.TaskActivityId = ValStep.TaskActivityId     
   where ValScr.IsDeleted = 0 and ValStep.IsDeleted = 0 order by ValStep.TaskActivityName asc
END

if(@Action = 'GelById')
Begin
  -- select * from tblMstExistingProcedure where ExistingProcedureId = @ExistingProcedureId and IsDeleted = 0
	if(@ScriptId !=0)
		 select ValScr.ScriptId,				ValScr.ScriptName, ValScr.ScriptPath,	ValStep.TaskActivityId,   ValStep.TaskActivityName        
		   from tblMstValitationScripts as ValScr inner join tblMstValidationSteps as ValStep on ValScr.TaskActivityId = ValStep.TaskActivityId     
		   where ValScr.IsDeleted = 0 and ScriptId=@ScriptId
	else 
	begin 
		SELECT * FROM tblMstValitationScripts --where TaskActivityId=@TaskActivityId and IsDeleted = 0
	end
END

if(@Action = 'Insert')
Begin
  INSERT INTO tblMstValitationScripts (ScriptName,ScriptPath,TaskActivityId,CreatedDate,ModifiedDate ) 
                                VALUES(@ScriptName,@ScriptPath,@TaskActivityId,getdate(),getdate())
END

if(@Action = 'Update')
Begin
   UPDATE tblMstValitationScripts SET ScriptName=@ScriptName,TaskActivityId =@TaskActivityId where ScriptId=@ScriptId
   --   UPDATE tblMstValidationSteps SET TaskActivityName='aaaaaa' where TaskActivityId=3
   -- select * from tblMstValidationSteps  
END

if(@Action = 'Delete')
Begin
   UPDATE tblMstValitationScripts SET IsDeleted=1, DeletedDate=GETDATE() where  ScriptId=@ScriptId
END

End
GO
