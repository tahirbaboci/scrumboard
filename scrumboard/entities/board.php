<?php
class board{

    // Connection instance
    private $connection;

    // table name
    private $table_name = "board";

    // table columns
    public $id;
    public $projectname;
    public $creationdate;
    public $status;


    public function __construct($connection){
        $this->connection = $connection;
    }

    //C
    public function create(){}
    //R
    public function read(){
      $query = "SELECT id, projectname FROM board";
      $stmt = $this->connection->prepare($query);
      $stmt->execute();
      return $stmt;
    }
    //U
    public function update(){}
    //D
    public function delete(){}
}
?>
