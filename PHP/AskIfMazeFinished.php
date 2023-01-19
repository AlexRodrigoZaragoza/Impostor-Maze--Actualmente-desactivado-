<?php

	require 'ConnectionSettings.php';
	//Variables submited by user
	$idMaze = $_POST["idMaze"];

	if(!$conn) {
	echo "la conexión no estaría funcionando<br/>";
	die("No pudo conectarse: " . $conn->connect_error);

	}else{

		//Preventing sql injections
		$statement = $conn->prepare("SELECT COMPLETED FROM table_maze WHERE ID = ?");
		$statement->bind_param("i",$idMaze);
		$statement->execute();		
		$result = $statement->get_result();
		$statement->close();

	    if($result->num_rows == 1)
	    {
	         $result = $result->fetch_array();
	         echo $result['COMPLETED'];
	    }
		
		$conn->close();
	}
?>