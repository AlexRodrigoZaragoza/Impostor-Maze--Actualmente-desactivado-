<?php

		require 'ConnectionSettings.php';

		//Variables submited by user
		$nick = $_POST["NICK"];
		$currentPoints = $_POST["CURRENT_POINTS"];
		$globalPoints = $_POST["GLOBAL_POINTS"];

			if(!$conn) {
			echo "la conexión no estaría funcionando<br/>";
			die("No pudo conectarse: " . $conn->connect_error);

			}else{
				//Preventing sql injections
					$sql = "UPDATE table_user SET GLOBAL_POINTS = $globalPoints, CURRENT_POINTS = $currentPoints WHERE ID = (SELECT ID FROM table_user WHERE NICK = $nick)";

				if ($conn->query($sql) === TRUE) {
				  echo "Record updated successfully";
				} else {
				  echo "Error updating record: " . $conn->error;
				}

				$conn->close();		
			}
?>