$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$authorizationValue = Read-Host -Prompt 'Input the authorization header value: '
$dateValue = Read-Host -Prompt 'Input the date header value: '
$url = Read-Host -Prompt 'Input the url: '

$headers.Add("Authorization", $authorizationValue)
$headers.Add("Date", $dateValue)

$response = Invoke-RestMethod $url -Headers $headers

Write-Host $response