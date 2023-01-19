<?php

require 'ConnectionSettings.php';

//Variables submited by user
$message= $_GET["message"];
$user = $_GET["user"];
$position = $_GET["position"];
$chunk = $_GET["chunk"];
$date = $_GET["date"];
$idMaze = $_GET["idMaze"];

	if(!$conn) {
	echo "la conexión no estaría funcionando<br/>";
	die("No pudo conectarse: " . conn->connect_error);

	}else{
		//Preventing sql injections
			$statement = $conn->prepare("INSERT INTO table_message(MESSAGE,USER,POSITION,CHUNK,DATE,MAZE) VALUES(?,?,?,?,?,?)");

			$statement->bind_param('sisisi',$message, $user, $position, $chunk, $date, $idMaze);

			if($statement->execute()){
				//Devolver mensaje
				echo "New sign has been created";
			}else{
				echo "Error: " .$statement . "<br>" . $conn->error;
			}
			$statement->close();
			$conn->close();		
	}
?>