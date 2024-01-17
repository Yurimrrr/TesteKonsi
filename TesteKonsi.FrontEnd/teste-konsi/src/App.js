import React, { useState } from 'react';
import axios from 'axios';

import logo from './logo.svg';
import './App.css';

function App() {

  const [query, setQuery] = useState('');
  const [data, setData] = useState(null);

  
  const searchAPI = async () => {
    try {
      const response = await axios.get(`http://localhost:5081/api/v1/benefits?cpf=${query}`);
      setData(response.data);
    } catch (error) {
      console.error("Erro ao buscar dados da API", error);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <div>
          <input 
            type="text" 
            value={query} 
            onChange={e => setQuery(e.target.value)} 
            placeholder="Digite sua pesquisa aqui" 
          />
          <button onClick={searchAPI}>Pesquisar</button>
          {data && <div>{JSON.stringify(data)}</div>}
        </div>
      </header>
    </div>
  );
}

export default App;