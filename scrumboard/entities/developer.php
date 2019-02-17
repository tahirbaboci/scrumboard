<?php
class developer{

    // Connection instance
    private $connection;

    // table columns
    public $id;
    public $firstname;
    public $lastname;
    public $shortname;
    public $hash;
    public $salt;
    public $isloggedin;


    public function __construct($connection){
        $this->connection = $connection;
    }

    //C
    public function create(){

      try {
        // query to insert record
        $query = "INSERT INTO developers(firstname, lastname, shortname, hash, salt, isloggedin)
                            VALUES(:firstname, :lastname, :shortname, :hash, :salt, :isloggedin)";

        $stmt = $this->connection->prepare($query);

        $this->firstname=htmlspecialchars(strip_tags($this->firstname));
        $this->lastname=htmlspecialchars(strip_tags($this->lastname));
        $this->shortname=htmlspecialchars(strip_tags($this->shortname));
        $this->hash=htmlspecialchars(strip_tags($this->hash));
        $this->salt=htmlspecialchars(strip_tags($this->salt));
        $this->isloggedin=htmlspecialchars(strip_tags($this->isloggedin));

        $stmt->bindParam(":firstname", $this->firstname);
        $stmt->bindParam(":lastname", $this->lastname);
        $stmt->bindParam(":shortname", $this->shortname);
        $stmt->bindParam(":hash", $this->hash);
        $stmt->bindParam(":salt", $this->salt);
        $stmt->bindParam(":isloggedin", $this->isloggedin);
        $stmt->execute();

        return $stmt;

      } catch (PDOException $exception) {
        echo $exception;
      }
    }
    //R
    public function read(){
      $query = "SELECT id, firstname, lastname, shortname, hash, salt, isloggedin FROM developers";
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
