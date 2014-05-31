﻿/*
 Copyright 2014, Mark Taling
 
 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at
 
 http://www.apache.org/licenses/LICENSE-2.0
 
 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
*/

CREATE PROCEDURE [dbo].[PersistFeatureTestResult]
	@title VARCHAR(255),
	@branchName VARCHAR(255),
	@groupName VARCHAR(255),
	@serializedTestResult TEXT
AS
BEGIN
	UPDATE Feature 
	SET SerializedTestResult = @serializedTestResult
	WHERE Title = @title
	  AND BranchName = @branchName
	  AND GroupName = @groupName;
END
