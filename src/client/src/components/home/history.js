import React from "react";
import styled from "emotion/react";
import moment from "moment";

import Island from "../misc/island";

const HistoryLayout = styled(Island)`
  margin: 15px 0;
  max-height: 622px;
  overflow-y: scroll;
  padding: 0;
  background-color: rgba(0, 0, 0, 0.05);
  display: flex;
  flex-direction: column;
  @media only screen and (max-width: 600px) {
    max-width: 100vw;
  }
`;

const HistoryTitle = styled.div`
  padding-left: 12px;
  color: rgba(0, 0, 0, 0.4);
  font-size: 15px;
  line-height: 30px;
  text-transform: uppercase;
`;

const HistoryItem = styled.div`
  display: flex;
  justify-content: space-around;
  align-items: center;
  height: 74px;
  width: 620px;
  min-height: 74px;
  font-size: 15px;
  font-weight: bold;
  white-space: nowrap;

  &:nth-child(even) {
    background-color: #fff;
  }

  &:nth-child(odd) {
    background-color: rgba(255, 255, 255, 0.72);
  }

  color: ${({ isInvalid }) => (isInvalid ? "#F44336" : "#000")};
  
  @media only screen and (max-width: 960px) {
    flex-wrap: wrap;
    padding: 10px;
    width: auto;
   }
   @media only screen and (max-width: 906px) {
     height: 100px;
     min-height: 100px;
     justify-content: space-around;
   }
`;

const HistoryItemIcon = styled.div`
  width: 50px;
  height: 50px;
  border-radius: 25px;
  background-color: #159761;
  background-image: url(${({ bankSmLogoUrl }) => bankSmLogoUrl});
  background-size: contain;
  background-repeat: no-repeat;
`;

const HistoryItemTitle = styled.div`
  width: 360px;
  overflow: hidden;
  text-overflow: ellipsis;
  padding-left: 10px; 
   @media only screen and (max-width: 906px) {
    order: 5;
   }
`;

const HistoryItemTime = styled.div`
  width: 50px;
  @media only screen and (max-width: 906px) {
    margin-left: 10px;
  }
`;

const HistoryItemSum = styled.div`
  width: 72px;
  overflow: hidden;
  text-overflow: ellipsis;
  color: ${({ credit }) => (credit ? "red" : "green")};
`;

const History = ({ transactions, activeCard, isLoading }) => {
  const renderCardsHistory = () => {
    if (isLoading) return <HistoryItem>Загрузка...</HistoryItem>;

    const result = [];
    const today = moment().format("L");

    if (transactions.length === 0)
      result.push(
        <HistoryItem key={today + "HistoryItem"}>
          Операций не найдено
        </HistoryItem>
      );
    else {
      transactions.forEach(item => {
        if (item.key === today)
          result.push(<HistoryTitle key={item.key}>Сегодня</HistoryTitle>);
        else {
          if (result.length === 0) {
            result.push(
              <HistoryTitle key={today + "HistoryTitle"}>Сегодня</HistoryTitle>
            );
            result.push(
              <HistoryItem key={today + "HistoryItem"}>
                Операций за этот день нет
              </HistoryItem>
            );
          }

          result.push(<HistoryTitle key={item.key}>{item.key}</HistoryTitle>);
        }

        result.push(renderCardsDay(item.data));
      });
    }

    return result;
  };

  const renderCardsDay = arr =>
    arr.map(item => {
      return (
        <HistoryItem key={item.id}>
          <HistoryItemIcon bankSmLogoUrl={activeCard.theme.bankSmLogoUrl} />
          <HistoryItemTitle>{item.title}</HistoryItemTitle>
          <HistoryItemTime>{item.hhmm}</HistoryItemTime>
          <HistoryItemSum credit={item.credit}>
            {`${Number(item.sum.toFixed(2))} ${activeCard.currencySign}`}
          </HistoryItemSum>
        </HistoryItem>
      );
    });

  return <HistoryLayout>{renderCardsHistory()}</HistoryLayout>;
};

export default History;
