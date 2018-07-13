import * as action from './types';

import axios from 'axios';

const ROOT_URL = '/api';

/**
* Вытаскивает транзакции по картам пользователя
*
* @returns
*/
export const fetchTransactions = number => {
    return async dispatch => {
        try {
            dispatch({
                type: action.TRANS_FETCH_STARTED,
            });

            const response = await axios
                .get(`${ROOT_URL}/transactions/${number}`);

            dispatch({
                type: action.TRANS_FETCH_SUCCESS,
                payload: response.data
            });
        } catch (err) {
            dispatch({
                type: action.TRANS_FETCH_FAILED,
                payload: err.response.data.message ? err.response.data.message : err.response.data
            });
            console.log(err.response.data.message ? err.response.data.message : err.response.data);
        }
    }
}
