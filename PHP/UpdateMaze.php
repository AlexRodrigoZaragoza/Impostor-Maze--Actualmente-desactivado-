<?php

require 'ConnectionSettings.php';


//Variables submited by user
$idMaze = $_POST["idMaze"];
$completed = 1;

	if(!$conn) {
	echo "la conexión no estaría funcionando<br/>";
	die("No pudo conectarse: " . $conn->connect_error);

	}else{
		//Preventing sql injections
			$sql = "UPDATE table_maze SET COMPLETED = $completed WHERE ID = (SELECT ID FROM table_maze WHERE ID = $idMaze)";

if ($conn->query($sql) === TRUE) {
  echo "Record updated successfully";
} else {
  echo "Error updating record: " . $conn->error;
}

			$conn->close();		
	}
?>