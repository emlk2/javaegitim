require ("dotenv").config();
const {Pool} =require ("pg"); //pıstgresql baglantısı oluşşturmak için pg paketinde Pool özelligini alır

const pool=new Pool({
    user:process.env.DB_USER,
    host:process.env.DB_HOST,
    database:process.env.DB_NAME,
    password:process.envDB_PASSWORD,
    port:process.env.DB_PORT
});
module.exports=pool; //app.js bağlantıyı kullanabilsin




