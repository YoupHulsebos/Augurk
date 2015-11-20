Import-Module "$PSScriptRoot\..\BuildTaskTestHelper.psm1"

Describe "Publishes Features To Augurk" {
	InModuleScope BuildTaskTestHelper {
		$sut = "$PSScriptRoot\..\..\tfs-build-task\PublishFeaturesToAugurk\task.json"
		
		# Dummy function definitions so that we can Mock them
		Function Find-Files { }
		Function Get-ServiceEndpoint { }
		Function Invoke-Tool { }
		
		Mock Import-Module
		Mock Get-ServiceEndpoint { return "UrlToAugurkService" }
		
		Context "When Group Name Is Provided" {
			Mock Find-Files { return [PSCustomObject]@{FullName = "SomeInteresting.feature"} }
			Mock Invoke-Tool -Verifiable -ParameterFilter { $Arguments -like "*--groupName=SomeGroupName*" }
			$augurk = New-Item TestDrive:\augurk.exe -Type File
			
			Invoke-BuildTask -TaskDefinitionFile $sut -- -connectedServiceName "SomeAugurkService" -productName "SomeProduct" -version "SomeVersion" -groupName "SomeGroupName" 
				
			It "Calls augurk.exe with the provided group name" {
				Assert-VerifiableMocks
			}
		}
		
		Context "When Folder Structure is used" {
			Mock Find-Files { return [PSCustomObject]@{FullName = "SomeInteresting.feature"; Directory = "SomeParentFolder"} }
			Mock Invoke-Tool -Verifiable -ParameterFilter { $Arguments -like "*--groupName=SomeParentFolder*" }
			$augurk = New-Item TestDrive:\augurk.exe -Type File
			
			Invoke-BuildTask -TaskDefinitionFile $sut -- -connectedServiceName "SomeAugurkService" -productName "SomeProduct" -version "SomeVersion" -useFolderStructure "True"
				
			It "Calls augurk.exe with the provided group name" {
				Assert-VerifiableMocks
			}
		}
	}
}