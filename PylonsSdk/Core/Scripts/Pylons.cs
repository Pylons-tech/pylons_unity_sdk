using UnityEngine;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using PylonsSdk.Tx;
using Newtonsoft.Json;

public class Pylons : MonoBehaviour
{
    public static Pylons instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public delegate void CookbookEvent(object caller, Cookbook[] cookbooks);
    public delegate void ExecutionEvent(object caller, Execution[] executions);
    public delegate void ProfileEvent(object caller, Profile profile);
    public delegate void RecipeEvent(object caller, Recipe[] recipes);
    public delegate void TxEvent(object caller, Transaction[] txs);
    public delegate void WalletServiceTestEvent(object caller, string output);

    private void TxEventMessageHandler(IpcMessage msg, TxEvent evt) =>
        msg.Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((TxResponse)e).Transactions); } });

    public void ApplyRecipe(string recipe, string cookbook, string[] itemInputs, TxEvent evt) =>
       TxEventMessageHandler(new ApplyRecipe(recipe, cookbook, itemInputs), evt);

    public void CancelTrade(string tradeId, TxEvent evt) =>
        TxEventMessageHandler(new CancelTrade(tradeId), evt);

    public void CheckExecution(string execId, bool payForCompletion, TxEvent evt) =>
        TxEventMessageHandler(new CheckExecution(execId, payForCompletion), evt);

    public void CreateCookbooks(string[] ids, string[] names, string[] developers, string[] descriptions, string[] versions, string[] supportEmails, long[] levels, long[] costsPerBlock, TxEvent evt) =>
        TxEventMessageHandler(new CreateCookbooks(ids, names, developers, descriptions, versions, supportEmails, levels, costsPerBlock), evt);

    public void CreateRecipes(string[] names, string[] cookbooks, string[] decriptions, long[] blockIntervals, string[] coinInputs, string[] itemInputs, string[] outputTables, string[] outputs, TxEvent evt) =>
        TxEventMessageHandler(new CreateRecipes(names, cookbooks, decriptions, blockIntervals, coinInputs, itemInputs, outputTables, outputs), evt);

    public void CreateTrade(string[] coinInputs, string[] itemInputs, string[] coinOutputs, string[] itemOutputs, string extraInfo, TxEvent evt) =>
        TxEventMessageHandler(new CreateTrade(coinInputs, itemInputs, coinOutputs, itemOutputs, extraInfo), evt);

    public void DisableRecipes(string[] recipeIds, TxEvent evt) =>
        TxEventMessageHandler(new DisableRecipes(recipeIds), evt);

    public void EnableRecipes(string[] recipeIds, TxEvent evt) =>
        TxEventMessageHandler(new EnableRecipes(recipeIds), evt);

    public void FulfillTrade(string tradeId, string[] itemIds, TxEvent evt) =>
        TxEventMessageHandler(new FulfillTrade(tradeId, itemIds), evt);

    public void GetCookbooks(CookbookEvent evt) =>
        new GetCookbooks().Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((CookbookResponse)e).Cookbooks); } });

    public void GetPendingExecutions(ExecutionEvent evt) =>
        new GetPendingExecutions().Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((ExecutionResponse)e).Executions); } });

    public void GetProfile(string id, ProfileEvent evt) =>
        new GetProfile(id).Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((ProfileResponse)e).Profiles[0]); } });

    public void GetPylons(long count, TxEvent evt) =>
#if DEBUG
        TxEventMessageHandler(new GetPylons(count), evt);
#endif
    // outside of debug environments, we'll need to use a helper function to make the appropriate iap call
    // for the number of pylons given, get arguments for one of the iap-based backend calls, and then call that
    // here instead.
    // it may make more sense to do all iap on the wallet side so we can offload this logic, in which case a
    // separate GoogleIapGetPylons IPC call does not need to exist.

    public void GetRecipes(RecipeEvent evt) =>
        new GetRecipes().Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((RecipeResponse)e).Recipes); } });

    public void GetTransaction(string txHash, TxEvent evt) =>
        TxEventMessageHandler(new GetTransaction(txHash), evt);

    public void RegisterProfile(string name, TxEvent evt) =>
        TxEventMessageHandler(new RegisterProfile(name), evt);

    public void SendCoins(string denom, long count, string receiver, TxEvent evt) =>
        TxEventMessageHandler(new SendCoins(JsonConvert.SerializeObject(new Coin[] { new Coin(denom, count) }), receiver), evt);

    public void SetItemString(string item, string field, string value, TxEvent evt) =>
        TxEventMessageHandler(new SetItemString(item, field, value), evt);

    public void UpdateCookbooks(string[] ids, string[] names, string[] developers, string[] descriptions, string[] versions, string[] supportEmails, TxEvent evt) =>
        TxEventMessageHandler(new UpdateCookbooks(ids, names, developers, descriptions, versions, supportEmails), evt);

    public void UpdateRecipes(string[] ids, string[] names, string[] cookbooks, string[] decriptions, long[] blockIntervals, string[] coinInputs, string[] itemInputs, string[] outputTables, string[] outputs, TxEvent evt) =>
        TxEventMessageHandler(new UpdateRecipes(ids, names, cookbooks, decriptions, blockIntervals, coinInputs, itemInputs, outputTables, outputs), evt);    

    public void WalletServiceTest(string input, WalletServiceTestEvent evt) => 
        new WalletServiceTest(input).Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((TestResponse)e).Output); } });

    public void WalletUiTest(string input, WalletServiceTestEvent evt) =>
        new WalletUiTest(input).Broadcast(new IpcEvent[] { (s, e) => { evt.Invoke(s, ((TestResponse)e).Output); } });
}
