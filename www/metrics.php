<?php
    header('Access-Control-Allow-Origin: *');
    header('Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS');
    header('Access-Control-Allow-Headers: Content-Type');

    $hostname = "179.209.42.23";
    $port = "3306";
    $charset = "utf8mb4";
    $username = "teste123";
    $password = "119948b642a772402e9872798a119948b642a772402e9872789a";
    $dbname = "teste_db";

    // Create connection
    $dsn = "mysql:host=$hostname:$port;dbname=$dbname;charset=$charset";
    $options =
    [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
        PDO::ATTR_EMULATE_PREPARES   => false
    ];
    $pdo = new PDO($dsn, $username, $password, $options);

    if ($_SERVER['REQUEST_METHOD'] === 'GET')
    {
        $result = $pdo->prepare("SELECT * FROM metrics WHERE name = 'morte'");

        $result->execute();
        $data = $result->fetchAll();

        echo json_encode($data[0]);
    }
    else if ($_SERVER['REQUEST_METHOD'] === 'POST')
    {
        $data = json_decode(file_get_contents('php://input'), true);

        $result = $pdo->prepare("INSERT INTO metrics (name, value) VALUES (:name, :value)");

        $result->execute($data);

        echo json_encode(['message' => 'Métrica inserida com sucesso']);
    }