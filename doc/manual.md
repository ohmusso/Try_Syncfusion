# app manual

## static objects

## basic states

### StartUp

```yuml
// {type:activity}
// {generate:true}

(start)->(Configure)
(Configure)->(Build)
(Build)-><exist db?>
<exist db?>[db not exist]->(Create db with SeedData)

(Create db with SeedData)-><exist test users?>
<exist test users?>[test users not exist]->(Create test users)
(Create test users)->(run)
<exist test users?>[test users exist]->(run)


<exist db?>[db exist]->(run)

(run)->(end)

```
