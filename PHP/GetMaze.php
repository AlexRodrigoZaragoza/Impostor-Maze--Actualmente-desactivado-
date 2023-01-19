<?php

require 'ConnectionSettings.php';

if(!$conn) {
echo "la conexión no estaría funcionando<br/>";
die("No pudo conectarse: " . $conn->connect_error);

}else{
	//echo " Conectado ";
	//Preventing sql injections
	$statement = $conn->prepare("SELECT ID,SEED FROM table_maze WHERE COMPLETED = ?");
	$mazeCompleted = 0;
	$statement->bind_param("i",$mazeCompleted);
	$statement->execute();

	$result = $statement->get_result();

	$statement->close();
	
	if($result->num_rows > 0){
		//echo " A maze has been found ";
		$data = array();
		while($obj = $result->fetch_object()){
			$data[]=$obj;
		}
		echo json_encode($data);
	}else{
		echo " There isn't mazes to complete ";
	}
	
	$conn->close();
}
?>