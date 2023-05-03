import React, { createContext } from 'react';

const AppContext = createContext('');

const AppProvider = ({children} : any) => {
    return <AppContext.Provider value="Preetu">
        {children}
    </AppContext.Provider>
}

export default AppProvider;