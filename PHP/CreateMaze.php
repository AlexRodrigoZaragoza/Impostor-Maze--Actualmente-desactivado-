<?php

require 'ConnectionSettings.php';


//Variables submited by user
$seed = $_POST["seed"];

	if(!$conn) {
	echo "la conexión no estaría funcionando<br/>";
	die("No pudo conectarse: " . $conn->connect_error);

	}else{
		//Preventing sql injections
			$statement = $conn->prepare("INSERT INTO table_maze(SEED,COMPLETED) VALUES(?,?)");
			$completed = 0;
			$statement->bind_param('ii',$seed, $completed);

			if($statement->execute()){
				//Devolver laberinto
				echo "New maze created";
			}else{
				echo "Error: " .$statement . "<br>" . $conn->error;
			}
			$statement->close();
			$conn->close();		
	}
?>