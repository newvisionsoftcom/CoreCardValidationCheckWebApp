USE [CoreCard ]
GO
SET IDENTITY_INSERT [dbo].[tblMstCategory] ON 
GO
INSERT [dbo].[tblMstCategory] ([CategoryId], [CategoryName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, N'WCF', CAST(N'2021-10-11T15:25:13.877' AS DateTime), CAST(N'2021-10-11T15:25:13.877' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstCategory] ([CategoryId], [CategoryName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (2, N'BAT', CAST(N'2021-10-11T15:25:19.317' AS DateTime), CAST(N'2021-10-11T15:25:19.317' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstCategory] ([CategoryId], [CategoryName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (3, N'APP', CAST(N'2021-10-11T15:25:23.720' AS DateTime), CAST(N'2021-10-11T15:25:23.720' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstCategory] ([CategoryId], [CategoryName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (4, N'WEB', CAST(N'2021-10-11T15:25:28.870' AS DateTime), CAST(N'2021-10-11T15:25:28.870' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstCategory] ([CategoryId], [CategoryName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (5, N'KMS', CAST(N'2021-10-11T15:25:33.590' AS DateTime), CAST(N'2021-10-11T15:25:33.590' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMstComplexiety] ON 
GO
INSERT [dbo].[tblMstComplexiety] ([ComplexietyId], [ComplexietyName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, N'High
', CAST(N'2021-09-28T15:02:29.053' AS DateTime), CAST(N'2021-09-28T15:02:29.053' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstComplexiety] ([ComplexietyId], [ComplexietyName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (2, N'Medium
', CAST(N'2021-09-28T15:02:34.707' AS DateTime), CAST(N'2021-09-28T15:02:34.707' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstComplexiety] ([ComplexietyId], [ComplexietyName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (3, N'Complex
', CAST(N'2021-09-28T15:02:39.647' AS DateTime), CAST(N'2021-09-28T15:02:39.647' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstComplexiety] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMstExistingProcedure] ON 
GO
INSERT [dbo].[tblMstExistingProcedure] ([ExistingProcedureId], [ExistingProcedureName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, N'Config File verification', CAST(N'2021-10-11T15:25:49.980' AS DateTime), CAST(N'2021-10-11T15:25:49.980' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstExistingProcedure] ([ExistingProcedureId], [ExistingProcedureName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (2, N'Setup verification', CAST(N'2021-10-11T15:25:54.820' AS DateTime), CAST(N'2021-10-11T15:25:54.820' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstExistingProcedure] ([ExistingProcedureId], [ExistingProcedureName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (3, N'Browser level verification', CAST(N'2021-10-11T15:25:59.380' AS DateTime), CAST(N'2021-10-11T15:25:59.380' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstExistingProcedure] ([ExistingProcedureId], [ExistingProcedureName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (4, N'Windows service verification', CAST(N'2021-10-11T15:26:05.840' AS DateTime), CAST(N'2021-10-11T15:26:05.840' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstExistingProcedure] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMstFrequency] ON 
GO
INSERT [dbo].[tblMstFrequency] ([FrequencyId], [FrequencyName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, N'Daily', CAST(N'2021-09-28T15:02:51.000' AS DateTime), CAST(N'2021-09-28T15:02:51.000' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstFrequency] ([FrequencyId], [FrequencyName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (2, N'Weekly', CAST(N'2021-09-28T15:02:58.667' AS DateTime), CAST(N'2021-09-28T15:02:58.667' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstFrequency] ([FrequencyId], [FrequencyName], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (3, N'Monthly', CAST(N'2021-09-28T15:03:02.533' AS DateTime), CAST(N'2021-09-28T15:03:02.533' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstFrequency] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMstGroup] ON 
GO
INSERT [dbo].[tblMstGroup] ([GroupId], [GroupName], [Description], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, N'Group1', N'Desc Group1', CAST(N'2021-09-28T14:46:55.720' AS DateTime), CAST(N'2021-09-28T14:46:55.720' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMstValidationSteps] ON 
GO
INSERT [dbo].[tblMstValidationSteps] ([GroupId], [TaskActivityId], [TaskActivityName], [CategoryId], [ComplexietyId], [ExistingProcedureId], [FrequencyId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, 1, N'Verifying DB Server and DB names in WCF configuration file', 3, 1, 3, 1, CAST(N'2021-10-11T15:26:45.550' AS DateTime), CAST(N'2021-10-11T15:26:45.550' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstValidationSteps] ([GroupId], [TaskActivityId], [TaskActivityName], [CategoryId], [ComplexietyId], [ExistingProcedureId], [FrequencyId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, 2, N'Verifying DSLs, Platfrom, Batchcripts and Scheduled Jobs on Batch Servers', 2, 2, 1, 1, CAST(N'2021-10-11T15:26:57.970' AS DateTime), CAST(N'2021-10-11T15:26:57.970' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstValidationSteps] ([GroupId], [TaskActivityId], [TaskActivityName], [CategoryId], [ComplexietyId], [ExistingProcedureId], [FrequencyId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, 3, N'Verifying KMS Configuration on all KMS Servers', 5, 3, 2, 1, CAST(N'2021-10-11T15:27:19.747' AS DateTime), CAST(N'2021-10-11T15:27:19.747' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstValidationSteps] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMstValitationScripts] ON 
GO
INSERT [dbo].[tblMstValitationScripts] ([ScriptId], [ScriptName], [ScriptPath], [TaskActivityId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (1, N'PowerShell1.ps1', N'PowerShell1.ps1', 1, CAST(N'2021-10-11T15:29:06.330' AS DateTime), CAST(N'2021-10-11T15:29:06.330' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstValitationScripts] ([ScriptId], [ScriptName], [ScriptPath], [TaskActivityId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (2, N'PowerShell2.ps1', N'PowerShell2.ps1', 1, CAST(N'2021-10-11T15:29:06.330' AS DateTime), CAST(N'2021-10-11T15:29:06.330' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstValitationScripts] ([ScriptId], [ScriptName], [ScriptPath], [TaskActivityId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (3, N'PowerShell3.ps1', N'PowerShell3.ps1', 2, CAST(N'2021-10-11T15:29:13.290' AS DateTime), CAST(N'2021-10-11T15:29:13.290' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstValitationScripts] ([ScriptId], [ScriptName], [ScriptPath], [TaskActivityId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (4, N'PowerShell6.ps1', N'PowerShell6.ps1', 3, CAST(N'2021-10-11T15:29:23.850' AS DateTime), CAST(N'2021-10-11T15:29:23.850' AS DateTime), 0, NULL)
GO
INSERT [dbo].[tblMstValitationScripts] ([ScriptId], [ScriptName], [ScriptPath], [TaskActivityId], [CreatedDate], [ModifiedDate], [IsDeleted], [DeletedDate]) VALUES (5, N'PowerShell7.ps1', N'PowerShell7.ps1', 3, CAST(N'2021-10-11T15:29:23.863' AS DateTime), CAST(N'2021-10-11T15:29:23.863' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMstValitationScripts] OFF
GO
