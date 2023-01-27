import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import Home from '@/pages/Home';
import PageNotFound from '@/pages/Common/PagNotFound';

// berkeley page
import  BerkeleyLoginPage from '@can/pages/Login' 


// central page
import  CentralLoginPage from '@us/pages/Login'

export default function Routes() {
  return (
    <Router>
      <Switch>
        <Route exact path='/' component={Home} />
          <Route exact path = '/berkeley' component={BerkeleyLoginPage}/>
          <Route exact path= '/central' component={CentralLoginPage}/>
          <Route component={PageNotFound}/>
      </Switch>
    </Router>
  );
}
