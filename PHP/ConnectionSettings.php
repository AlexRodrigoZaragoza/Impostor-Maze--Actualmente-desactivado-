<?php
	//Data from database
	$serverName = "88.18.57.64";
	$db = "themaze";
	$uid = "admin";
	$pwd = "";

	$conn = new mysqli( $serverName, $uid, $pwd, $db);
	
	if(!$conn) {
		echo "la conexión no estaría funcionando<br/>";
		die("No pudo conectarse: " . $conn->connect_error);
	}
?>