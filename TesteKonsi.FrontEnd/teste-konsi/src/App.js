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
        <div>
          <div className="divPesquisa">
            <a>CPF:</a>
            <input 
              className='input'
              type="text" 
              value={query}
              onChange={e => setQuery(e.target.value)} 
              placeholder="Digite o CPF de pesquisa aqui" 
            />
            <button className='button' onClick={searchAPI}>Pesquisar</button>
          </div>
          
          {
            (data && data.benefits) && data.benefits.map((item, index) => (
              <>
                <div>
                  <a>Código benéficio: {item.benefitTypeCode} - </a> <a>Número do benéficio: {item.benefitNumer} </a>
                </div>
              </>
             ))
          }

        </div>
      </header>
    </div>
  );
}

export default App;