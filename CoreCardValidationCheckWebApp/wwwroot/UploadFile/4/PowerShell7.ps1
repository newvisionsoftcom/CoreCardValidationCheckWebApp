$HTTP_Request = [System.Net.WebRequest]::Create('http://google.com')

 

$HTTP_Response = $HTTP_Request.GetResponse()

 

$HTTP_Status = [int]$HTTP_Response.StatusCode

 

if ($HTTP_Status -eq 200) {
    Write-Host -ForegroundColor Green "Site is reachable"
}
Else {
    Write-Host -ForegroundColor Red "The Site may be down, please check!"
}

 

if ($HTTP_Response -eq $null) { } 
else { $HTTP_Response.Close() }